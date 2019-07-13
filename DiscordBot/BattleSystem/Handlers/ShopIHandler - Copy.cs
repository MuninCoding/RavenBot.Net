using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.BattleSystem.Handlers
{
    public class ShopIHandler
    {
        internal static async Task Potions(SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);
            //var itemToAdd = (IPotion)Activator.CreateInstance(itemName);
            var botMessage = await context.Channel.SendMessageAsync("not implemented yet.");
            await Task.Delay(5000);
            await botMessage.DeleteAsync();
        }

        internal static async Task Weapons(Type itemName, SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);
            var itemToAdd = (IWeapon)Activator.CreateInstance(itemName);

            if (!userAccount.BattleStatistics.Weapons.Any(x => x.Name == itemToAdd.Name))
            {
                if (userAccount.BattleStatistics.Gold >= itemToAdd.PurchasePrice)
                {
                    userAccount.BattleStatistics.Weapons.Add(itemToAdd);
                    userAccount.BattleStatistics.Gold -= itemToAdd.PurchasePrice;
                    var botMessage = await context.Channel.SendMessageAsync($"You bought a {itemToAdd.Name} for {itemToAdd.PurchasePrice} Gold.");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync($"You have not enough to buy a {itemToAdd.Name} for {itemToAdd.PurchasePrice} Gold.");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync($"{itemToAdd.Name} is already in your inventory");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();
            }
        }

        internal static async Task Armors(Type itemName, SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);
            var itemToAdd = (IArmor)Activator.CreateInstance(itemName);

            if (!userAccount.BattleStatistics.Armors.Any(x => x.Name == itemToAdd.Name))
            {
                if (userAccount.BattleStatistics.Gold >= itemToAdd.PurchasePrice)
                {
                    userAccount.BattleStatistics.Armors.Add(itemToAdd);
                    userAccount.BattleStatistics.Gold -= itemToAdd.PurchasePrice;
                    var botMessage = await context.Channel.SendMessageAsync($"You bought a {itemToAdd.Name} for {itemToAdd.PurchasePrice} Gold.");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync($"You have not enough to buy a {itemToAdd.Name} for {itemToAdd.PurchasePrice} Gold.");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync($"{itemToAdd.Name} is already in your inventory");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();
            }
        }

        internal static async Task Shields(Type itemName, SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);
            var itemToAdd = (IShield)Activator.CreateInstance(itemName);

            if (!userAccount.BattleStatistics.Shields.Any(x => x.Name == itemToAdd.Name))
            {
                if (userAccount.BattleStatistics.Gold >= itemToAdd.PurchasePrice)
                {
                    userAccount.BattleStatistics.Shields.Add(itemToAdd);
                    userAccount.BattleStatistics.Gold -= itemToAdd.PurchasePrice;
                    var botMessage = await context.Channel.SendMessageAsync($"You bought a {itemToAdd.Name} for {itemToAdd.PurchasePrice} Gold.");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync($"You have not enough to buy a {itemToAdd.Name} for {itemToAdd.PurchasePrice} Gold.");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync($"{itemToAdd.Name} is already in your inventory");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();
            }
        }

        internal static async Task Points(SocketCommandContext context)
        {
            var userAccount = UserManager.GetAccount(context.Message.Author);
            var botMessage = await context.Channel.SendMessageAsync("not implemented yet.");
            await Task.Delay(5000);
            await botMessage.DeleteAsync();
        }
    }
}
