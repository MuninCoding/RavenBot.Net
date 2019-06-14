using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class BattleModule : ModuleBase<SocketCommandContext>
    {
        [Command("battle")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Battle(SocketGuildUser user)
        {
            if(user != null)
            {
                var account = UserManager.GetAccount(user);
                
            }
            else
            {
                await ReplyAsync("Please @ a username!");
            }
           
        }

        [Command("bbs")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task BattleStats(SocketGuildUser user = null)
        {
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
            }

            var embed = new EmbedBuilder
            {
                Title = "Help Window",
                Description = "Displays all commands"
            };

            // Or with methods
            //TODO UPDATE
            embed.WithColor(Color.Blue)
                 .WithTitle("Battle Stats")
                 .WithDescription("Get information of your battle stats")
                 .AddField("Health", account.BattleStatistics.Health.ToString(), true)
                 .AddField("Attack", account.BattleStatistics.Attack.ToString(), true)
                 .AddField("Defense", account.BattleStatistics.Defense.ToString(), true)
                 .AddField("Level", account.BattleStatistics.Level.ToString(), true)
                 .AddField("XP", account.BattleStatistics.Xp.ToString(), true)
                 .AddField("Skill Points", account.BattleStatistics.SkillPoints.ToString(), true)
                 .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                 .WithCurrentTimestamp();

            var info = embed.Build();

            await ReplyAsync(embed: info);

        }

    }
}
