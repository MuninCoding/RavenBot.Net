﻿using System;
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
using Discord.WebSocket;

namespace DiscordBot.BattleSystem.Handlers
{
    public class ItemHandler
    {
        internal static async Task AddItem(string itemTypeString, Type itemType, SocketCommandContext context, SocketUser user = null)
        {
            //Create empty field to set the user to
            SocketUser userToAdd;

            //If there was a user passed in the function call
            if (user != null)
            {
                //Set the passed user as user to add
                userToAdd = user;
            }
            else
            {
                //Else set the message author as user to add
                userToAdd = context.Message.Author;
            }

            //Then we can get the account of the desired user
            var userAccount = UserManager.GetAccount(userToAdd);

            if (itemTypeString.Equals("weapon"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Weapons.Add((IWeapon)itemToAdd);

                var botMessage = await context.Channel.SendMessageAsync($"Added {itemTypeString} of type {itemType.ToString()}");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

            }
            else if (itemTypeString.Equals("shield"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Shields.Add((IShield)itemToAdd);

                var botMessage = await context.Channel.SendMessageAsync($"Added {itemTypeString} of type {itemType.ToString()}");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

            }
            else if (itemTypeString.Equals("armor"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Armors.Add((IArmor)itemToAdd);

                var botMessage = await context.Channel.SendMessageAsync($"Added {itemTypeString} of type {itemType.ToString()}");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("Item type not found");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

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

                var botMessage = await context.Channel.SendMessageAsync($"Equipped {itemTypeString} of type {itemType.ToString()}");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

            }
            else if (itemTypeString.Equals("shield"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Shield = (IShield)itemToAdd;

                var botMessage = await context.Channel.SendMessageAsync($"Equipped {itemTypeString} of type {itemType.ToString()}");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

            }
            else if (itemTypeString.Equals("armor"))
            {
                var itemToAdd = Activator.CreateInstance(itemType);

                userAccount.BattleStatistics.Armor = (IArmor)itemToAdd;

                var botMessage = await context.Channel.SendMessageAsync($"Equipped {itemTypeString} of type {itemType.ToString()}");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("Item type not found");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();

            }
            UserManager.SaveAccounts();
        }
    }
}

