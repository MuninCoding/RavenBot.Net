using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities.Armor;
using DiscordBot.BattleSystem.Entities.Shield;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.BattleSystem.Handlers;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class BattlePointsModule : ModuleBase<SocketCommandContext>
    {
        [Command("skill")]
        public async Task UseBattlepoints(string type)
        {
            var user = UserManager.GetAccount(Context.Message.Author);

            if (user.BattleStatistics.BattlePoints >= 1)
            {
                if (user.BattleStatistics.Level <= 5)
                {
                    if (type.Equals("health"))
                    {
                        user.BattleStatistics.BaseHealth += 10;
                        await ReplyAsync("You increased your Health +10 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("damage"))
                    {
                        user.BattleStatistics.BaseDamage += 10;
                        await ReplyAsync("You increased your Damage +10 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("defense"))
                    {
                        user.BattleStatistics.BaseDefense += 10;
                        await ReplyAsync("You increased your Defense +10 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else
                    {
                        await ReplyAsync("Please define a type.");
                    }
                }
                else if (user.BattleStatistics.Level <= 10)
                {
                    if (type.Equals("health"))
                    {
                        user.BattleStatistics.BaseHealth += 15;
                        await ReplyAsync("You increased your Health +15 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("damage"))
                    {
                        user.BattleStatistics.BaseDamage += 15;
                        await ReplyAsync("You increased your Damage +15 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("defense"))
                    {
                        user.BattleStatistics.BaseDefense += 15;
                        await ReplyAsync("You increased your Defense +15 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else
                    {
                        await ReplyAsync("Please define a type.");
                    }
                }
                else if (user.BattleStatistics.Level <= 15)
                {
                    if (type.Equals("health"))
                    {
                        user.BattleStatistics.BaseHealth += 20;
                        await ReplyAsync("You increased your Health +20 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("damage"))
                    {
                        user.BattleStatistics.BaseDamage += 20;
                        await ReplyAsync("You increased your Damage +20 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("defense"))
                    {
                        user.BattleStatistics.BaseDefense += 20;
                        await ReplyAsync("You increased your Defense +20 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else
                    {
                        await ReplyAsync("Please define a type.");
                    }
                }
                else if (user.BattleStatistics.Level >= 20)
                {
                    if (type.Equals("health"))
                    {
                        user.BattleStatistics.BaseHealth += 25;
                        await ReplyAsync("You increased your Health +25 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("damage"))
                    {
                        user.BattleStatistics.BaseDamage += 25;
                        await ReplyAsync("You increased your Damage +25 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else if (type.Equals("defense"))
                    {
                        user.BattleStatistics.BaseDefense += 25;
                        await ReplyAsync("You increased your Defense +25 ");
                        user.BattleStatistics.BattlePoints--;
                    }
                    else
                    {
                        await ReplyAsync("Please define a type.");
                    }
                }
            }
            else
            {
                await ReplyAsync("You have not enough Battle Points to skill anything");
            }
        }
    }
}
