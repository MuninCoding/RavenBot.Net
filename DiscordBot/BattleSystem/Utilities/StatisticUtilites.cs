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
                embed.WithColor(Color.DarkRed);
                embed.WithTitle("LEVEL UP!");
                embed.WithDescription(context.Message.Author.Username + " just leveled up!");
                embed.AddField("LEVEL", newLevel);
                embed.AddField("Battle XP", account.BattleStatistics.Xp);
                var info = embed.Build();
                await context.Channel.SendMessageAsync(embed: info);
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForCreepWinstreak(uint currentCreepWinStreak, uint highestCreepWinStreak, SocketCommandContext context, UserAccount account)
        {
            if (currentCreepWinStreak > highestCreepWinStreak)
            {
                account.BattleStatistics.HighestCreepWinStreak = account.BattleStatistics.CurrentCreepWinStreak;
                await context.Channel.SendMessageAsync($"You get a new Creep Winstreak with {account.BattleStatistics.HighestCreepWinStreak}!");
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForPvpWinstreak(uint currentPvPWinStreak, uint highestPvPWinStreak, SocketCommandContext context, UserAccount account)
        {
            if (currentPvPWinStreak > highestPvPWinStreak)
            {
                account.BattleStatistics.HighestPvpWinStreak = account.BattleStatistics.CurrentPvpWinStreak;
                await context.Channel.SendMessageAsync($"You get a new PvP Winstreak with {account.BattleStatistics.HighestPvpWinStreak}!");
                return true;
            }
            else
            {
                return false;
            }

        }
        internal static async Task<bool> CheckForBossWinStreak(uint currentBossWinStreak, uint highestBossWinStreak, SocketCommandContext context, UserAccount account)
        {
            if (currentBossWinStreak > highestBossWinStreak)
            {
                account.BattleStatistics.HighestBossWinStreak = account.BattleStatistics.CurrentBossWinStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Boss Killingstreak with {account.BattleStatistics.HighestCreepKillStreak} Kills !");
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForEnemiesKilled(uint currentEnemiesKilled, uint highestEnemiesKilled, SocketCommandContext context, UserAccount account)
        {
            if (currentEnemiesKilled > highestEnemiesKilled)
            {
                account.BattleStatistics.HighestCreepKillStreak = account.BattleStatistics.CurrentCreepKillStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Enemies {account.BattleStatistics.HighestCreepKillStreak} Killingstreak!");
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForPlayerKills(uint currentPlayerKilled, uint highestPlayerKilled, SocketCommandContext context, UserAccount account)
        {
            if (currentPlayerKilled > highestPlayerKilled)
            {
                account.BattleStatistics.HighestPvpKillStreak = account.BattleStatistics.CurrentPvpKillStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Enemies {account.BattleStatistics.HighestCreepKillStreak} Killingstreak!");
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static void RewriteHighscores()
        {
            List<UserAccount> accounts = UserManager.GetAccounts();

            List<UserAccount> accountsSortedByLevel = accounts.OrderByDescending(x => x.BattleStatistics.Level).ToList();
            for (int i = 0; i < accountsSortedByLevel.Count; i++)
            {
                accountsSortedByLevel[i].BattleStatistics.LeaderboardPositionLevel = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByXp = accounts.OrderByDescending(x => x.BattleStatistics.Xp).ToList();
            for (int i = 0; i < accountsSortedByXp.Count; i++)
            {
                accountsSortedByXp[i].BattleStatistics.LeaderboardPositionXp = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByBattlepoints = accounts.OrderByDescending(x => x.BattleStatistics.BattlePoints).ToList();
            for (int i = 0; i < accountsSortedByBattlepoints.Count; i++)
            {
                accountsSortedByBattlepoints[i].BattleStatistics.LeaderboardPositionBattlepoints = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByCreeepKills = accounts.OrderByDescending(x => x.BattleStatistics.AmountOfCreepsKilled).ToList();
            for (int i = 0; i < accountsSortedByCreeepKills.Count; i++)
            {
                accountsSortedByCreeepKills[i].BattleStatistics.LeaderboardPositionCreepKills = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByBossKills = accounts.OrderByDescending(x => x.BattleStatistics.AmountOfBossesKilled).ToList();
            for (int i = 0; i < accountsSortedByBossKills.Count; i++)
            {
                accountsSortedByBossKills[i].BattleStatistics.LeaderboardPositionBossKills = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByPlayerKills = accounts.OrderByDescending(x => x.BattleStatistics.AmountOfPlayersKilled).ToList();
            for (int i = 0; i < accountsSortedByPlayerKills.Count; i++)
            {
                accountsSortedByPlayerKills[i].BattleStatistics.LeaderboardPositionPvpKills = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByCreepDrops = accounts.OrderByDescending(x => x.BattleStatistics.CreepDrops).ToList();
            for (int i = 0; i < accountsSortedByCreepDrops.Count; i++)
            {
                accountsSortedByCreepDrops[i].BattleStatistics.LeaderboardPositionCreepDrops = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByBossDrops = accounts.OrderByDescending(x => x.BattleStatistics.BossDrops).ToList();
            for (int i = 0; i < accountsSortedByBossDrops.Count; i++)
            {
                accountsSortedByBossDrops[i].BattleStatistics.LeaderboardPositionBossDrops = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByPvpDrops = accounts.OrderByDescending(x => x.BattleStatistics.PvpDrops).ToList();
            for (int i = 0; i < accountsSortedByPvpDrops.Count; i++)
            {
                accountsSortedByPvpDrops[i].BattleStatistics.LeaderboardPositionPvpDrops = (uint)i + 1;
            }

            UserManager.SaveAccounts();
        }
        //internal static async Task<bool> CheckForBestCreepKiller()
        //{

        //}
    }
}