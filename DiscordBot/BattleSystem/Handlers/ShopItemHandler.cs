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
    public class ShopItemHandler
    {
        internal static async Task BuyItem(string itemSlot, Type type, SocketCommandContext context, UserAccount account)
        {
            if (itemSlot.Equals("weapon"))
            {
                var itemToBuy = (IWeapon)Activator.CreateInstance(type);
                if (account.BattleStatistics.Gold >= itemToBuy.PurchasePrice)
                {
                    if (!account.BattleStatistics.Weapons.Any(x => x.Name == itemToBuy.Name))
                    {
                        account.BattleStatistics.Weapons.Add(itemToBuy);
                        account.BattleStatistics.Gold -= itemToBuy.PurchasePrice;

                        var botMessage = await context.Channel.SendMessageAsync($"You bought a {type.Name} for {itemToBuy.PurchasePrice} Gold");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                    else
                    {
                        var botMessage = await context.Channel.SendMessageAsync($"{type.Name} already in inventory");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                }
                else
                {
                    await context.Channel.SendMessageAsync($"You do have not enough Gold to buy a {type.Name}");
                }
            }
            else if (itemSlot.Equals("armor"))
            {
                var itemToBuy = (IArmor)Activator.CreateInstance(type);
                if (account.BattleStatistics.Gold >= itemToBuy.PurchasePrice)
                {
                    if (!account.BattleStatistics.Armors.Any(x => x.Name == itemToBuy.Name))
                    {
                        account.BattleStatistics.Armors.Add(itemToBuy);
                        account.BattleStatistics.Gold -= itemToBuy.PurchasePrice;

                        var botMessage = await context.Channel.SendMessageAsync($"You bought a {type.Name} for {itemToBuy.PurchasePrice} Gold");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                    else
                    {
                        var botMessage = await context.Channel.SendMessageAsync($"{type.Name} already in inventory");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                }
                else
                {
                    await context.Channel.SendMessageAsync($"You do have not enough Gold to buy a {type.Name}");
                }
            }
            else if (itemSlot.Equals("shield"))
            {
                var itemToBuy = (IShield)Activator.CreateInstance(type);
                if (account.BattleStatistics.Gold >= itemToBuy.PurchasePrice)
                {
                    if (!account.BattleStatistics.Shields.Any(x => x.Name == itemToBuy.Name))
                    {
                        account.BattleStatistics.Shields.Add(itemToBuy);
                        account.BattleStatistics.Gold -= itemToBuy.PurchasePrice;

                        var botMessage = await context.Channel.SendMessageAsync($"You bought a {type.Name} for {itemToBuy.PurchasePrice} Gold");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                    else
                    {
                    var botMessage = await context.Channel.SendMessageAsync($"{type.Name} already in inventory");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                    }                   
                }
                else
                {
                    await context.Channel.SendMessageAsync($"You do have not enough Gold to buy a {type.Name}");
                }
            }
            else if (itemSlot.Equals("potion"))
            {
                var itemToBuy = (IPotion)Activator.CreateInstance(type);
                if (account.BattleStatistics.Gold >= itemToBuy.PurchasePrice)
                {
                    if (account.BattleStatistics.PotionAmount <=5)
                    {
                        account.BattleStatistics.Potions.Add(itemToBuy);
                        account.BattleStatistics.Gold -= itemToBuy.PurchasePrice;

                        var botMessage = await context.Channel.SendMessageAsync($"You bought a {type.Name} for {itemToBuy.PurchasePrice} Gold");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                    else
                    {
                        var botMesssage = await context.Channel.SendMessageAsync($"You have already 5 potions you cant buy another one");
                        await Task.Delay(5000);
                        await botMesssage.DeleteAsync();
                    }
                }
                else
                {
                    await context.Channel.SendMessageAsync($"You do have not enough Gold to buy a {type.Name}");
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("Item type not found");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();
            }
        }
    }
}
