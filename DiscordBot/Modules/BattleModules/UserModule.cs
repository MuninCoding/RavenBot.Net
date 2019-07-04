using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Handlers;
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
        [Command("battlestats")]
        [Alias("bs")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task BattleStats(string stats, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
                if (stats.Equals("player"))
                {                   
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Player Stats")
                         .AddField("Health", account.BattleStatistics.Health.ToString(), true)
                         .AddField("Damage", account.BattleStatistics.Damage.ToString(), true)
                         .AddField("Defense", account.BattleStatistics.Defense.ToString(), true)
                         .AddField("Level", account.BattleStatistics.Level.ToString(), true)
                         .AddField("Battle XP", account.BattleStatistics.Xp.ToString(), true)
                         .AddField("Battle Points", account.BattleStatistics.BattlePoints.ToString(), true)
                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var playerEmbed = embed.Build();
                    await ReplyAsync(embed: playerEmbed);
                }
                else if (stats.Equals("creep"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Creep Stats")
                         .AddField("Creep Battles Fought", account.BattleStatistics.CreepBattlesFought.ToString(), true)
                         .AddField("Creep Battles Won", account.BattleStatistics.CreepBattlesWon.ToString(), true)
                         .AddField("Creep Battles Lost", account.BattleStatistics.CreepBattlesLost.ToString(), true)
                         .AddField("Current Creep Winstreak", account.BattleStatistics.CurrentCreepWinStreak.ToString(), true)
                         .AddField("Current Creep Killstreak", account.BattleStatistics.CurrentCreepKillStreak.ToString(), true)
                         .AddField("Totally killed creeps", account.BattleStatistics.AmountOfCreepsKilled.ToString(), true)
                         .AddField("Highest Creep Winstreak", account.BattleStatistics.HighestCreepWinStreak.ToString(), true)
                         .AddField("Highest Creep Killstreak", account.BattleStatistics.HighestCreepKillStreak.ToString(), true)
                         .AddField("Creep Rank placement", account.BattleStatistics.LeaderboardPositionCreepKills.ToString(), true)
                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
                else if (stats.Equals("boss"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Boss Stats")
                         .AddField("Boss Battles Fought", account.BattleStatistics.BossBattlesFought.ToString(), true)
                         .AddField("Boss Battles Won", account.BattleStatistics.BossBattlesWon.ToString(), true)   
                         .AddField("Boss Battles Lost", account.BattleStatistics.BossBattlesLost.ToString(), true)
                         .AddField("Current Boss Winstreak", account.BattleStatistics.CurrentBossWinStreak.ToString(), true)
                         .AddField("Current Boss Killstreak", account.BattleStatistics.CurrentBossKillStreak.ToString(), true)
                         .AddField("Totally killed Bosses", account.BattleStatistics.AmountOfBossesKilled.ToString(), true)
                         .AddField("Highest Boss Winstreak", account.BattleStatistics.HighestBossWinStreak.ToString(), true)
                         .AddField("Highest Boss Killstreak", account.BattleStatistics.HighestBossKillStreak.ToString(), true)
                         .AddField("Boss Rank placement", account.BattleStatistics.LeaderboardPositionBossKills.ToString(), true)

                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
                else if (stats.Equals("pvp"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("PvP Stats")
                         .AddField("PVP Battles Fought", account.BattleStatistics.PvPBattlesFought.ToString(), true)
                         .AddField("PVP Battles Won", account.BattleStatistics.PvPBattlesWon.ToString(), true)
                         .AddField("PVP Battles Lost", account.BattleStatistics.PvPBattlesLost.ToString(), true)
                         .AddField("Current PVP Winstreak", account.BattleStatistics.CurrentPvpWinStreak.ToString(), true)
                         .AddField("Current PVP Killstreak", account.BattleStatistics.CurrentPvpKillStreak.ToString(), true)
                         .AddField("Totally killed Players", account.BattleStatistics.AmountOfPlayersKilled.ToString(), true)
                         .AddField("Highest PVP Winstreak", account.BattleStatistics.HighestPvpWinStreak.ToString(), true)
                         .AddField("Highest Players Killstreak", account.BattleStatistics.HighestPvpKillStreak.ToString(), true)
                         .AddField("PVP Rank placement", account.BattleStatistics.LeaderboardPositionPvpKills.ToString(), true)


                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
                else if (stats.Equals("best"))
                {
                    List<UserAccount> userAccounts = UserManager.GetAccounts();

                    //Filter accoutns by Leaderboard position one
                    var levelAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionLevel == 1);
                    //Get the remaining object from the list to use its information in the embed
                    var hightestLevelAccount = levelAccountList.SingleOrDefault();

                    var xpAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionXp == 1);
                    var hightestXpAccount = xpAccountList.SingleOrDefault();

                    var skillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionBattlepoints == 1);
                    var hightestSkillAccount = skillAccountList.SingleOrDefault();

                    var creepKillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionCreepKills == 1);
                    var hightestCreepKillAccount = creepKillAccountList.SingleOrDefault();

                    var bossKillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionBossKills == 1);
                    var hightestBossKillAccount = bossKillAccountList.SingleOrDefault();

                    var pvpKillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionPvpKills == 1);
                    var hightestPvpKillAccount = pvpKillAccountList.SingleOrDefault();

                    var creepDropAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionCreepDrops == 1);
                    var hightestCreepDropAccount = creepDropAccountList.SingleOrDefault();

                    var bossDropAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionBossDrops == 1);
                    var hightestBossDropAccount = bossDropAccountList.SingleOrDefault();

                    var pvpDropAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionPvpDrops == 1);
                    var hightestPvpDropAccount = pvpDropAccountList.SingleOrDefault();

                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Highscores")
                         .AddField("Highest Level", $"{hightestLevelAccount.Name} with {hightestLevelAccount.LevelNumber} Levels")
                         .AddField("Highest XP", $"{hightestXpAccount.Name} with {hightestXpAccount.BattleStatistics.Xp}")
                         .AddField("Highest Battlepoints", $"{hightestSkillAccount.Name} with {hightestSkillAccount.BattleStatistics.BattlePoints}")
                         .AddField("Highest Creeps Killed", $"{hightestCreepKillAccount.Name} with {hightestCreepKillAccount.BattleStatistics.AmountOfCreepsKilled}")
                         .AddField("Highest Bosses Killed", $"{hightestBossKillAccount.Name} with {hightestBossKillAccount.BattleStatistics.AmountOfBossesKilled}")
                         .AddField("Highest Players Killed", $"{hightestPvpKillAccount.Name} with {hightestPvpKillAccount.BattleStatistics.AmountOfPlayersKilled}")
                         .AddField("Highest Creep Drops", $"{hightestCreepDropAccount.Name} with {hightestCreepDropAccount.BattleStatistics.CreepDrops}")
                         .AddField("Highest Boss Drops", $"{hightestBossDropAccount.Name} with {hightestBossDropAccount.BattleStatistics.BossDrops}")
                         .AddField("Highest PvP Drops", $"{hightestPvpDropAccount.Name} with {hightestPvpDropAccount.BattleStatistics.PvpDrops}")
                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();
                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
                if (stats.Equals("player"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Player Stats")
                         .AddField("Health", account.BattleStatistics.Health.ToString(), true)
                         .AddField("Damage", account.BattleStatistics.Damage.ToString(), true)
                         .AddField("Defense", account.BattleStatistics.Defense.ToString(), true)
                         .AddField("Level", account.BattleStatistics.Level.ToString(), true)
                         .AddField("Battle XP", account.BattleStatistics.Xp.ToString(), true)
                         .AddField("Battle Points", account.BattleStatistics.BattlePoints.ToString(), true)
                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var playerEmbed = embed.Build();
                    await ReplyAsync(embed: playerEmbed);
                }
                else if (stats.Equals("creep"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Creep Stats")
                         .AddField("Creep Battles Fought", account.BattleStatistics.CreepBattlesFought.ToString(), true)
                         .AddField("Creep Battles Won", account.BattleStatistics.CreepBattlesWon.ToString(), true)
                         .AddField("Creep Battles Lost", account.BattleStatistics.CreepBattlesLost.ToString(), true)
                         .AddField("Current Creep Winstreak", account.BattleStatistics.CurrentCreepWinStreak.ToString(), true)
                         .AddField("Current Creep Killstreak", account.BattleStatistics.CurrentCreepKillStreak.ToString(), true)
                         .AddField("Totally killed creeps", account.BattleStatistics.AmountOfCreepsKilled.ToString(), true)
                         .AddField("Highest Creep Winstreak", account.BattleStatistics.HighestCreepWinStreak.ToString(), true)
                         .AddField("Highest Creep Killstreak", account.BattleStatistics.HighestCreepKillStreak.ToString(), true)
                         .AddField("Creep Rank placement", account.BattleStatistics.LeaderboardPositionCreepKills.ToString(), true)
                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
                else if (stats.Equals("boss"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Boss Stats")
                         .AddField("Boss Battles Fought", account.BattleStatistics.BossBattlesFought.ToString(), true)
                         .AddField("Boss Battles Won", account.BattleStatistics.BossBattlesWon.ToString(), true)
                         .AddField("Boss Battles Lost", account.BattleStatistics.BossBattlesLost.ToString(), true)
                         .AddField("Current Boss Winstreak", account.BattleStatistics.CurrentBossWinStreak.ToString(), true)
                         .AddField("Current Boss Killstreak", account.BattleStatistics.CurrentBossKillStreak.ToString(), true)
                         .AddField("Totally killed Bosses", account.BattleStatistics.AmountOfBossesKilled.ToString(), true)
                         .AddField("Highest Boss Winstreak", account.BattleStatistics.HighestBossWinStreak.ToString(), true)
                         .AddField("Highest Boss Killstreak", account.BattleStatistics.HighestBossKillStreak.ToString(), true)
                         .AddField("Boss Rank placement", account.BattleStatistics.LeaderboardPositionBossKills.ToString(), true)

                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
                else if (stats.Equals("pvp"))
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("PvP Stats")
                         .AddField("PVP Battles Fought", account.BattleStatistics.PvPBattlesFought.ToString(), true)
                         .AddField("PVP Battles Won", account.BattleStatistics.PvPBattlesWon.ToString(), true)
                         .AddField("PVP Battles Lost", account.BattleStatistics.PvPBattlesLost.ToString(), true)
                         .AddField("Current PVP Winstreak", account.BattleStatistics.CurrentPvpWinStreak.ToString(), true)
                         .AddField("Current PVP Killstreak", account.BattleStatistics.CurrentPvpKillStreak.ToString(), true)
                         .AddField("Totally killed Players", account.BattleStatistics.AmountOfPlayersKilled.ToString(), true)
                         .AddField("Highest PVP Winstreak", account.BattleStatistics.HighestPvpWinStreak.ToString(), true)
                         .AddField("Highest Players Killstreak", account.BattleStatistics.HighestPvpKillStreak.ToString(), true)
                         .AddField("PVP Rank placement", account.BattleStatistics.LeaderboardPositionPvpKills.ToString(), true)


                         .WithFooter(footer => footer.Text = "©DivineGuardian")
                         .WithCurrentTimestamp();

                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
                else if (stats.Equals("best"))
                {
                    List<UserAccount> userAccounts = UserManager.GetAccounts();

                    var levelAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionLevel == 1);
                    var hightestLevelAccount = levelAccountList.SingleOrDefault();

                    var xpAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionXp == 1);
                    var hightestXpAccount = xpAccountList.SingleOrDefault();

                    var skillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionBattlepoints == 1);
                    var hightestSkillAccount = skillAccountList.SingleOrDefault();

                    var creepKillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionCreepKills == 1);
                    var hightestCreepKillAccount = creepKillAccountList.SingleOrDefault();

                    var bossKillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionBossKills == 1);
                    var hightestBossKillAccount = bossKillAccountList.SingleOrDefault();

                    var pvpKillAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionPvpKills == 1);
                    var hightestPvpKillAccount = pvpKillAccountList.SingleOrDefault();

                    var creepDropAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionCreepDrops == 1);
                    var hightestCreepDropAccount = creepDropAccountList.SingleOrDefault();

                    var bossDropAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionBossDrops == 1);
                    var hightestBossDropAccount = bossDropAccountList.SingleOrDefault();

                    var pvpDropAccountList = userAccounts.Where(x => x.BattleStatistics.LeaderboardPositionPvpDrops == 1);
                    var hightestPvpDropAccount = pvpDropAccountList.SingleOrDefault();

                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed)
                         .WithTitle("Highscores")
                         .AddField("Highest Level", $"{hightestLevelAccount.Name} with {hightestLevelAccount.LevelNumber} Levels")
                         .AddField("Highest XP", $"{hightestXpAccount.Name} with {hightestXpAccount.BattleStatistics.Xp}")
                         .AddField("Highest Battlepoints", $"{hightestSkillAccount.Name} with {hightestSkillAccount.BattleStatistics.BattlePoints}")
                         .AddField("Highest Creeps Killed", $"{hightestCreepKillAccount.Name} with {hightestCreepKillAccount.BattleStatistics.AmountOfCreepsKilled}")
                         .AddField("Highest Bosses Killed", $"{hightestBossKillAccount.Name} with {hightestBossKillAccount.BattleStatistics.AmountOfBossesKilled}")
                         .AddField("Highest Players Killed", $"{hightestPvpKillAccount.Name} with {hightestPvpKillAccount.BattleStatistics.AmountOfPlayersKilled}")
                         .AddField("Highest Creep Drops", $"{hightestCreepDropAccount.Name} with {hightestCreepDropAccount.BattleStatistics.CreepDrops}")
                         .AddField("Highest Boss Drops", $"{hightestBossDropAccount.Name} with {hightestBossDropAccount.BattleStatistics.BossDrops}")
                         .AddField("Highest PvP Drops", $"{hightestPvpDropAccount.Name} with {hightestPvpDropAccount.BattleStatistics.PvpDrops}")
                         .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                         .WithCurrentTimestamp();
                    var creepEmbed = embed.Build();
                    await ReplyAsync(embed: creepEmbed);
                }
            }
        }

        [Command("addbattlepoints")]
        [Alias("abp")]
        public async Task AddBattlePoints(uint amount, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
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
            await Context.Message.DeleteAsync();
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

        [Command("addbattlexp")]
        [Alias("abxp")]
        public async Task AddBattleXp(uint amount, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
                account.BattleStatistics.Xp += amount;
                await ReplyAsync($"{Context.Message.Author} add {amount} BattleXp {user.Mention} Account!");

            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
                account.BattleStatistics.Xp += amount;
                await ReplyAsync($"You add {amount} BattleXp to your Account!");
            }
            UserManager.SaveAccounts();
        }

        [Command("removebattlexp")]
        [Alias("rbxp")]
        public async Task RemoveBattleXp(uint amount, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
                account.BattleStatistics.Xp -= amount;
                await ReplyAsync($"{Context.Message.Author} removed {amount} BattleXp {user.Mention} Account!");
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
                account.BattleStatistics.Xp -= amount;
                await ReplyAsync($"You removed {amount} BattleXp from your Account!");
            }
            UserManager.SaveAccounts();
        }

    }
}
