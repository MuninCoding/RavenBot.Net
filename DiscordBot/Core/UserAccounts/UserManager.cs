using Discord.WebSocket;
using DiscordBot.BattleSystem;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.Armor;
using DiscordBot.BattleSystem.Entities.Shield;
using DiscordBot.BattleSystem.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.UserAccounts
{
    public static class UserManager
    {
        private static List<UserAccount> accounts;

        private static string accountsFile = "Resources/accounts.json";

        public static object BattleStatistics { get; private set; }

        static UserManager()
        {
            if (DataStorage.SaveExists(accountsFile))
            {
                accounts = DataStorage.LoadUserManager(accountsFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            DataStorage.SaveUserManager(accounts, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id, user.Username);
        }

        private static UserAccount GetOrCreateAccount(ulong id, string userName)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;

            var account = result.FirstOrDefault();
            if (account == null)
            {
                account = CreateUserAccount(id, userName);
            }
            else
            {
                account.Name = userName;
            }
            return account;
        }

        private static UserAccount CreateUserAccount(ulong id, string userName)
        {
            PlayerStatistics battleStats = new PlayerStatistics();
            battleStats.BattlePoints = 0;
            battleStats.BaseDamage = 0;
            battleStats.BaseDefense = 0;
            battleStats.BaseHealth = 100;      
            battleStats.Weapon = new Fist();
            battleStats.Shield = new HandBlock();
            battleStats.Armor = new Naked();
            battleStats.Weapons = new List<IWeapon>() { new Fist() };
            battleStats.Shields = new List<IShield>() { new HandBlock() };
            battleStats.Armors = new List<IArmor>() { new Naked() };
            var newAccount = new UserAccount()
            {
                ID = id,
                Name = userName,
                BattleStatistics = battleStats,
                XP = 0
            };

            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }

        internal static List<UserAccount> GetAccounts()
        {
            return accounts;
        }
    }

}