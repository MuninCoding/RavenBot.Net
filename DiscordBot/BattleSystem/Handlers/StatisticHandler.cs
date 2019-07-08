﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.BattleSystem.Handlers
{
    class StatisticHandler
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
                account.BattleStatistics.CreepStatistics.HighestCreepWinStreak = account.BattleStatistics.CreepStatistics.CurrentCreepWinStreak;
                await context.Channel.SendMessageAsync($"You get a new Creep Winstreak with {account.BattleStatistics.CreepStatistics.HighestCreepWinStreak}!");
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForCreepKillStreak(uint currentCreepKillStreak, uint highestCreepKillStreak, SocketCommandContext context, UserAccount account)
        {
            if (currentCreepKillStreak > highestCreepKillStreak)
            {
                account.BattleStatistics.CreepStatistics.HighestCreepKillStreak = account.BattleStatistics.CreepStatistics.CurrentCreepKillStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Enemies {account.BattleStatistics.CreepStatistics.HighestCreepKillStreak} Killingstreak!");
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
                account.BattleStatistics.BossStatistics.HighestBossWinStreak = account.BattleStatistics.BossStatistics.CurrentBossWinStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Boss Killingstreak with {account.BattleStatistics.BossStatistics.HighestBossKillStreak} Kills !");
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
                account.BattleStatistics.PvpStatistics.HighestPvpWinStreak = account.BattleStatistics.PvpStatistics.CurrentPvpWinStreak;
                await context.Channel.SendMessageAsync($"You get a new PvP Winstreak with {account.BattleStatistics.PvpStatistics.HighestPvpWinStreak}!");
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
                account.BattleStatistics.PvpStatistics.HighestPvpKillStreak = account.BattleStatistics.PvpStatistics.CurrentPvpKillStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Enemies {account.BattleStatistics.PvpStatistics.HighestPvpKillStreak} Killingstreak!");
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static void RewriteHighscores()
        {
            //Get all useraccounts
            List<UserAccount> accounts = UserManager.GetAccounts();

            //Sort the accounts by descending order(100->0) and make a list out of it with toList()
            List<UserAccount> accountsSortedByLevel = accounts.OrderByDescending(x => x.BattleStatistics.Level).ToList();
            //Loop over the sorted array
            for (int i = 0; i < accountsSortedByLevel.Count; i++)
            {
                //And set the leaderboard position to index +1 because we want the first place to be 1 not 0
                accountsSortedByLevel[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionLevel = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByXp = accounts.OrderByDescending(x => x.BattleStatistics.Xp).ToList();
            for (int i = 0; i < accountsSortedByXp.Count; i++)
            {
                accountsSortedByXp[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionXp = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByBattlepoints = accounts.OrderByDescending(x => x.BattleStatistics.BattlePoints).ToList();
            for (int i = 0; i < accountsSortedByBattlepoints.Count; i++)
            {
                accountsSortedByBattlepoints[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionBattlepoints = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByCreeepKills = accounts.OrderByDescending(x => x.BattleStatistics.CreepStatistics.AmountOfCreepsKilled).ToList();
            for (int i = 0; i < accountsSortedByCreeepKills.Count; i++)
            {
                accountsSortedByCreeepKills[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionCreepKills = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByBossKills = accounts.OrderByDescending(x => x.BattleStatistics.BossStatistics.AmountOfBossesKilled).ToList();
            for (int i = 0; i < accountsSortedByBossKills.Count; i++)
            {
                accountsSortedByBossKills[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionBossKills = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByPlayerKills = accounts.OrderByDescending(x => x.BattleStatistics.PvpStatistics.AmountOfPlayersKilled).ToList();
            for (int i = 0; i < accountsSortedByPlayerKills.Count; i++)
            {
                accountsSortedByPlayerKills[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionPvpKills = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByCreepDrops = accounts.OrderByDescending(x => x.BattleStatistics.DropStatistics.CreepDrops).ToList();
            for (int i = 0; i < accountsSortedByCreepDrops.Count; i++)
            {
                accountsSortedByCreepDrops[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionCreepDrops = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByBossDrops = accounts.OrderByDescending(x => x.BattleStatistics.DropStatistics.BossDrops).ToList();
            for (int i = 0; i < accountsSortedByBossDrops.Count; i++)
            {
                accountsSortedByBossDrops[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionBossDrops = (uint)i + 1;
            }

            List<UserAccount> accountsSortedByPvpDrops = accounts.OrderByDescending(x => x.BattleStatistics.DropStatistics.PvpDrops).ToList();
            for (int i = 0; i < accountsSortedByPvpDrops.Count; i++)
            {
                accountsSortedByPvpDrops[i].BattleStatistics.LeaderboardStatistics.LeaderboardPositionPvpDrops = (uint)i + 1;
            }

            UserManager.SaveAccounts();
        }
    }
}