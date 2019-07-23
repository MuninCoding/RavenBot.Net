using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBot.BattleSystem.Entities.Potions;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.BattleSystem.Handlers
{
    public class PotionHandler
    {
        internal static async Task DivinePotion(UserAccount account, SocketCommandContext context)
        {
            if (account.BattleStatistics.Potions.Any(x => x.Name == "Divine Potion"))
            {
                if (account.BattleStatistics.IsDead)
                {
                    account.BattleStatistics.CurrentHealth = account.BattleStatistics.BaseHealth;
                    var potion = account.BattleStatistics.Potions.First(x => x.Name == "Divine Potion");
                    account.BattleStatistics.Potions.Remove(potion);
                    var botMessage = await context.Channel.SendMessageAsync("You are alive");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                    UserManager.SaveAccounts();
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync("You are not Dead");
                    await Task.Delay(10000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("You have no Divine Potion to use");
                await Task.Delay(10000);
                await botMessage.DeleteAsync();
            }
        }

        internal static async Task BigHealingPotion(UserAccount account, SocketCommandContext context)
        {
            if (account.BattleStatistics.Potions.Any(x => x.Name == "Big Healing Potion"))
            {
                if (account.BattleStatistics.CurrentHealth != account.BattleStatistics.BaseHealth)
                {
                    account.BattleStatistics.CurrentHealth += 75;
                    var potion = account.BattleStatistics.Potions.First(x => x.Name == "Big Healing Potion");
                    account.BattleStatistics.Potions.Remove(potion);
                    if (account.BattleStatistics.CurrentHealth > account.BattleStatistics.BaseHealth)
                    {
                        account.BattleStatistics.CurrentHealth = account.BattleStatistics.BaseHealth;
                        var botMessage = await context.Channel.SendMessageAsync("health is now full!");
                        await Task.Delay(10000);
                    }
                    else
                    {
                        var botMessage = await context.Channel.SendMessageAsync($"your current health is now {account.BattleStatistics.CurrentHealth} from {account.BattleStatistics.BaseHealth}");
                        await Task.Delay(10000);
                        await botMessage.DeleteAsync();
                    }
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync("You cant use a potion because your life is already full");
                    await Task.Delay(10000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("you have no Big Healing Potion to use!");
                await Task.Delay(10000);
                await botMessage.DeleteAsync();
            }
        }

        internal static async Task MidHealingPotion(UserAccount account, SocketCommandContext context)
        {
            if (account.BattleStatistics.Potions.Any(x => x.Name == "MId Healing Potion"))
            {
                if (account.BattleStatistics.CurrentHealth != account.BattleStatistics.BaseHealth)
                {
                    account.BattleStatistics.CurrentHealth += 50;
                    var potion = account.BattleStatistics.Potions.First(x => x.Name == "Mid Healing Potion");
                    account.BattleStatistics.Potions.Remove(potion);
                    if (account.BattleStatistics.CurrentHealth > account.BattleStatistics.BaseHealth)
                    {
                        account.BattleStatistics.CurrentHealth = account.BattleStatistics.BaseHealth;
                        var botMessage = await context.Channel.SendMessageAsync("health is now full!");
                        await Task.Delay(10000);
                    }
                    else
                    {
                        var botMessage = await context.Channel.SendMessageAsync($"your current health is now {account.BattleStatistics.CurrentHealth} from {account.BattleStatistics.BaseHealth}");
                        await Task.Delay(10000);
                        await botMessage.DeleteAsync();
                    }
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync("You cant use a potion because your life is already full");
                    await Task.Delay(10000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("you have no Big Healing Potion to use!");
                await Task.Delay(10000);
                await botMessage.DeleteAsync();
            }

        }

        internal static async Task LilHealingPotion(UserAccount account, SocketCommandContext context)
        {
            if (account.BattleStatistics.Potions.Any(x => x.Name == "Lil Healing Potion"))
            {
                if (account.BattleStatistics.CurrentHealth != account.BattleStatistics.BaseHealth)
                {
                    account.BattleStatistics.CurrentHealth += 25;
                    var potion = account.BattleStatistics.Potions.First(x => x.Name == "Lil Healing Potion");
                    account.BattleStatistics.Potions.Remove(potion);
                    if (account.BattleStatistics.CurrentHealth > account.BattleStatistics.BaseHealth)
                    {
                        account.BattleStatistics.CurrentHealth = account.BattleStatistics.BaseHealth;
                        var botMessage = await context.Channel.SendMessageAsync("health is now full!");
                        await Task.Delay(10000);
                    }
                    else
                    {
                        var botMessage = await context.Channel.SendMessageAsync($"your current health is now {account.BattleStatistics.CurrentHealth} from {account.BattleStatistics.BaseHealth}");
                        await Task.Delay(10000);
                        await botMessage.DeleteAsync();
                    }
                }
                else
                {
                    var botMessage = await context.Channel.SendMessageAsync("You cant use a potion because your life is already full");
                    await Task.Delay(10000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("you have no Big Healing Potion to use!");
                await Task.Delay(10000);
                await botMessage.DeleteAsync();
            }
        }
    }
}
