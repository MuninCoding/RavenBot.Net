using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.BattleSystem.Utilities
{
    class StatisticUtilites
    {
        /*CheckForLevelUp is a static method 
        returns bool if user leveledUp
        arguments oldLevel of accounts, new Level of accounts, commandcontex and account of the user*/
        internal static async Task<bool> CheckForLevelUp(uint oldLevel, uint newLevel, SocketCommandContext context, UserAccount account)
        {
            if (oldLevel < newLevel)
            {
                account.BattleStatistics.BattlePoints++;
                var embed = new EmbedBuilder();
                embed.WithColor(67, 160, 71);
                embed.WithTitle("LEVEL UP!");
                embed.WithDescription(context.Message.Author.Username + " just leveled up!");
                embed.AddField("LEVEL", newLevel);
                embed.AddField("XP", account.BattleStatistics.Xp);
                var info = embed.Build();
                await context.Channel.SendMessageAsync(embed: info);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
