using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
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
                    account.BattleStatistics.CurrentHealth = account.BattleStatistics.Health;
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
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();
                }
            }
            else
            {
                var botMessage = await context.Channel.SendMessageAsync("You have no Divine Potion to use");
                await Task.Delay(5000);
                await botMessage.DeleteAsync();
            }
        }

        internal static Task HealingPotion(UserAccount account, SocketCommandContext context)
        {
            throw new NotImplementedException();
        }
    }
}
