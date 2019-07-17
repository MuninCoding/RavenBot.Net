using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.Potions;
using DiscordBot.BattleSystem.Handlers;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class UsePotionModule : ModuleBase<SocketCommandContext>
    {
        [Command("use")]
        public async Task UsePotion(string potionName)
        {           
            var account = UserManager.GetAccount(Context.User);

            if (potionName.Equals("divine"))
            {
                if (account.BattleStatistics.Potions.Any(x => x.Name == "Divine Potion"))
                {
                    if (account.BattleStatistics.IsDead)
                    {
                        account.BattleStatistics.CurrentHealth = account.BattleStatistics.Health;
                        var potion = account.BattleStatistics.Potions.First(x => x.Name == "Divine Potion");
                        account.BattleStatistics.Potions.Remove(potion);
                        var botMessage = await Context.Channel.SendMessageAsync("You are alive");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                        UserManager.SaveAccounts();
                    }
                    else
                    {
                        var botMessage = await Context.Channel.SendMessageAsync("You are not Dead");
                        await Task.Delay(5000);
                        await botMessage.DeleteAsync();
                    }
                }
                else if (potionName.Equals("healing"))
                {
                    account.BattleStatistics.CurrentHealth += H
                }
                else
                {
                    var botMessage = await Context.Channel.SendMessageAsync("You have no potion to use");
                    await Task.Delay(5000);
                    await botMessage.DeleteAsync();

                }
            }
        }
    }
}
