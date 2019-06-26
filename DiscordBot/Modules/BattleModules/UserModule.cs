using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class UserModule : ModuleBase<SocketCommandContext>
    {
        int DelayInMs = 1000; 
        [Command("battlestats")]
        [Alias("bs")]
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

            var embed = new EmbedBuilder(); 

            embed.WithColor(Color.Blue)
                 .WithTitle("Battle Stats")
                 .WithDescription("Get information of your battle stats")
                 .AddField("Health", account.BattleStatistics.Health.ToString(), true)
                 .AddField("Damage", account.BattleStatistics.Damage.ToString(), true)
                 .AddField("Defense", account.BattleStatistics.Defense.ToString(), true)
                 .AddField("Level", account.BattleStatistics.Level.ToString(), true)
                 .AddField("XP", account.BattleStatistics.Xp.ToString(), true)
                 .AddField("Battle Points", account.BattleStatistics.BattlePoints.ToString(), true)
                 .AddField("Creep Battles Fought", account.BattleStatistics.CreepBattlesFought.ToString(), true)
                 .AddField("Creep Battles Won", account.BattleStatistics.CreepBattlesWon.ToString(), true)
                 .AddField("Creep Battles Lost", account.BattleStatistics.CreepBattlesLost.ToString(), true)
                 .AddField("PvP Battles Fought", account.BattleStatistics.PvPBattlesFought.ToString(), true)
                 .AddField("PvP Battles Won", account.BattleStatistics.PvPBattlesWon.ToString(), true)
                 .AddField("PvP Battles Lost", account.BattleStatistics.PvPBattlesLost.ToString(), true)

                 .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                 .WithCurrentTimestamp();

            var info = embed.Build();

            await ReplyAsync(embed: info);

        }

        [Command("addbattlepoints")]
        [Alias("abp")]
        public async Task AddBattlePoints(uint amount, SocketGuildUser user = null)
        {
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
                account.BattleStatistics.BattlePoints += amount;
                await ReplyAsync($"{Context.Message.Author} add {amount} Battle Points {user.Mention} Account!");
                
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
                account.BattleStatistics.BattlePoints += amount;
                await ReplyAsync($"You add {amount} Battle Points to your Account!");
            }
            UserManager.SaveAccounts();
        }

        [Command("removebattlepoints")]
        [Alias("rbp")]

        public async Task RemoveBattlePoints(uint amount, SocketGuildUser user = null)
        {
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
                account.BattleStatistics.BattlePoints -= amount;
                await ReplyAsync($"{Context.Message.Author} removed {amount} Battle Points {user.Mention} Account!");
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
                account.BattleStatistics.BattlePoints -= amount;
                await ReplyAsync($"You removed {amount} Battle Points from your Account!");
            }
            UserManager.SaveAccounts();
        }
    }
}
