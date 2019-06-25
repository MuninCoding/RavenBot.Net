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
using DiscordBot.BattleSystem.Entities.Shield;
using DiscordBot.BattleSystem.Entities.Armor;

namespace DiscordBot.BattleSystem.Utilities
{
    public class ItemUtilities
    {
        internal static async Task AddItem(string itemTypeString, Type itemType, SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);

            if (itemTypeString.Equals("weapon"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Weapons.Add((IWeapon)itemToAdd);

                await context.Channel.SendMessageAsync($"Added {itemTypeString} of type {itemType.ToString()}");
            }
            else if (itemTypeString.Equals("shield"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Shields.Add((IShield)itemToAdd);

                await context.Channel.SendMessageAsync($"Added {itemTypeString} of type {itemType.ToString()}");
            }
            else if (itemTypeString.Equals("armor"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Armors.Add((IArmor)itemToAdd);

                await context.Channel.SendMessageAsync($"Added {itemTypeString} of type {itemType.ToString()}");
            }
            else
            {
                await context.Channel.SendMessageAsync("Item type not found");
            }

            UserManager.SaveAccounts();
        }

        internal static async Task EquipItem(string itemTypeString, Type itemType, SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);

            if (itemTypeString.Equals("weapon"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Weapon = (IWeapon)itemToAdd;

                await context.Channel.SendMessageAsync($"Equipped {itemTypeString} of type {itemType.ToString()}");
            }
            else if (itemTypeString.Equals("shield"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Shield = (IShield)itemToAdd;

                await context.Channel.SendMessageAsync($"Equipped {itemTypeString} of type {itemType.ToString()}");
            }
            else if (itemTypeString.Equals("armor"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Armor = (IArmor)itemToAdd;

                await context.Channel.SendMessageAsync($"Equipped {itemTypeString} of type {itemType.ToString()}");
            }
            else
            {
                await context.Channel.SendMessageAsync("Item type not found");
            }

            UserManager.SaveAccounts();
        }
    }
}

