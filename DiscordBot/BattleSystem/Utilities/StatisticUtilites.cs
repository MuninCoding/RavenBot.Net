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
                embed.AddField("Battle XP", account.BattleStatistics.BattleXp);
                var info = embed.Build();
                await context.Channel.SendMessageAsync(embed: info);
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForCreepWinstreak(uint CurrentCreepWinStreak, uint BestCreepWinStreak, SocketCommandContext context, UserAccount account)
        {
            if (CurrentCreepWinStreak > BestCreepWinStreak)
            {
                account.BattleStatistics.BestCreepWinStreak = account.BattleStatistics.CurrentCreepWinStreak;
                await context.Channel.SendMessageAsync($"You get a new Creep Winstreak with {account.BattleStatistics.BestCreepWinStreak}!");
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static async Task<bool> CheckForPvpWinstreak(uint CurrentPvPWinStreak, uint BestPvPWinStreak, SocketCommandContext context, UserAccount account)
        {
            if (CurrentPvPWinStreak > BestPvPWinStreak)
            {
                account.BattleStatistics.BestPvPWinStreak = account.BattleStatistics.CurrentPvPWinStreak;
                await context.Channel.SendMessageAsync($"You get a new PvP Winstreak with {account.BattleStatistics.BestPvPWinStreak}!");
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
                account.BattleStatistics.HighestEnemiesKilled = account.BattleStatistics.CurrentEnemiesKilled;
                await context.Channel.SendMessageAsync($"Your scored a new Enemies {account.BattleStatistics.HighestEnemiesKilled} Killingstreak!");
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
                account.BattleStatistics.HighestPlayerKillStreak = account.BattleStatistics.CurrentPlayerKillStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Enemies {account.BattleStatistics.HighestEnemiesKilled} Killingstreak!");
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}