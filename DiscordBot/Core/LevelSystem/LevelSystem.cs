using Discord;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.LevelSystem
{
    internal static class LevelSystem
    {
        internal static async void AddXpForMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            // if the user has a timeout, ignore them
            // TODO: This feature should be implemented
            //          by a viewer.
            
            var userAccount = UserManager.GetAccount(user);
            uint oldLevel = userAccount.LevelNumber;
            userAccount.XP += 5;
            UserManager.SaveAccounts();
            uint newLevel = userAccount.LevelNumber;

            if (oldLevel != newLevel)
            {
                // the user leveled up
                var embed = new EmbedBuilder();
                embed.WithColor(67, 160, 71);
                embed.WithTitle("LEVEL UP!");
                embed.WithDescription(user.Username + " just leveled up!");
                embed.AddField("LEVEL", newLevel);
                embed.AddField("XP", userAccount.XP);
                var info = embed.Build();
                await channel.SendMessageAsync(embed: info);
                userAccount.BattleStatistics.BattlePoints += 0.25;
                UserManager.SaveAccounts();
            }
        }
    }
}
