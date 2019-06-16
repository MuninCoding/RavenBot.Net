using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.Core.UserAccounts;
using Discord;
using Discord.Commands;

namespace DiscordBot.BattleSystem
{
    public class BattleUtilities
    {
        internal static async Task EquipWeapon(string weaponName, Discord.Commands.SocketCommandContext context)
        {
            UserAccount account = UserManager.GetAccount(context.Message.Author);
            List<IWeapon> weapons = account.BattleStatistics.Weapons;

            if (weaponName.Equals("rock"))
            {
                foreach (IWeapon weapon in weapons)
                {
                    if (weapon.Name.Equals("Rock"))
                    {
                        account.BattleStatistics.Weapon = new Rock();
                        await context.Channel.SendMessageAsync("Rock equipped");
                    }
                }
            }
            else if (weaponName.Equals("bat"))
            {
                foreach (IWeapon weapon in weapons)
                {
                    if (weapon.Name.Equals("Bat"))
                    {
                        account.BattleStatistics.Weapon = new Bat();
                        await context.Channel.SendMessageAsync("Bat equipped");
                    }
                }
            }
            else
            {
                await context.Channel.SendMessageAsync("Weapon not found");
            }
        }

        internal static Task EquipShield(string shieldName, SocketCommandContext context)
        {
            throw new NotImplementedException();
        }

        internal static Task EquipArmor(string armorName, SocketCommandContext context)
        {
            throw new NotImplementedException();
        }
    }
}
