using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.BattleSystem.Handlers
{
    class StatisticHandler
    {
        /*CheckForLevelUp is a static method 
        returns bool if user leveledUp
        arguments oldLevel of accounts, new Level of accounts, commandcontex and account of the user*/
        internal static async Task<(bool leveledUp, uint messageCount)> CheckForLevelUp(uint oldLevel, uint newLevel, SocketCommandContext context, UserAccount account, uint messageCount)
        {
            messageCount = await CheckForBossWave(context, account, messageCount);

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
                messageCount++;
                return (true, messageCount);
            }
            else
            {
                return (false,  messageCount);
            }
        }

        private static async Task<uint> CheckForBossWave(SocketCommandContext context, UserAccount account, uint messageCount)
        {
            bool isNewBossWinStreak = false;
            bool isNewHighestBossKillStreak = false;

            if (account.BattleStatistics.Level % 5 == 0 && !account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined)
            {
                
                bool wonBossFight = false;

                var channel = await context.Message.Author.GetOrCreateDMChannelAsync();
                await channel.SendMessageAsync($"A boss appeared, do you want to fight it?");
                await Task.Delay(5000);

                var dmMessages = await channel.GetMessagesAsync(1).FlattenAsync();
                foreach (var message in dmMessages)
                {
                    if (message.Content.Equals("yes"))
                    {
                        List<IEnemy>enemies = SpawnHandler.SpawnEnemies(account.BattleStatistics.Level, account.BattleStatistics.Damage, true);
                        var fightResult = await FarmHandler.SimulateFight(enemies, account, channel, 0);
                        wonBossFight = fightResult.isWinner;

                        account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined = true;

                        if (wonBossFight)
                        {
                            await channel.SendMessageAsync("You Won this Boss Fight!");
                            await channel.SendMessageAsync("You gained 100 Xp for your Win! Congrats");

                            var oldLevel = account.BattleStatistics.Level;
                            account.BattleStatistics.Xp += 100;
                            var newLevel = account.BattleStatistics.Level;

                            var levelResult = await CheckForLevelUp(oldLevel, newLevel, context, account, messageCount);
                            bool leveledUp = levelResult.leveledUp;
                            messageCount = levelResult.messageCount;
                             

                            uint currentBossWinStreak = account.BattleStatistics.BossStatistics.CurrentBossWinStreak;
                            uint highestBossWinStreak = account.BattleStatistics.BossStatistics.HighestBossWinStreak;
                            isNewBossWinStreak = await CheckForBossWinStreak(currentBossWinStreak, highestBossWinStreak, context, account);
                            if (isNewBossWinStreak)
                                messageCount++;

                            uint currentBossKills = account.BattleStatistics.BossStatistics.CurrentBossKillStreak;
                            uint highestBossKills = account.BattleStatistics.BossStatistics.HighestBossKillStreak;
                            isNewHighestBossKillStreak = await CheckForBossKillStreak(currentBossKills, highestBossKills, context, account);
                            if (isNewHighestBossKillStreak)
                                messageCount++;

                            return messageCount;
                        }
                        else
                        {
                            await channel.SendMessageAsync("You lost the Boss battle, Good luck next time!");
                            messageCount++;

                            account.BattleStatistics.BossStatistics.BossBattlesFought++;
                            account.BattleStatistics.BossStatistics.BossBattlesLost++;
                            account.BattleStatistics.BossStatistics.CurrentBossWinStreak = 0;
                            account.BattleStatistics.BossStatistics.CurrentBossKillStreak = 0;

                            return messageCount;
                        }
                    }
                    else if (message.Content.Equals("no"))
                    {
                        await channel.SendMessageAsync("You declined the Boss battle, Good luck next time!");
                        messageCount++;

                        account.BattleStatistics.BossStatistics.BossBattlesDeclined++;
                        account.BattleStatistics.BossStatistics.CurrentBossWinStreak = 0;
                        account.BattleStatistics.BossStatistics.CurrentBossKillStreak = 0;
                        account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined = true;

                        return messageCount;
                    }
                }
            }
            else if(account.BattleStatistics.Level % 5 == 1)
            {
                account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined = false;

                return messageCount;
            }

            return messageCount;
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
        internal static async Task<bool> CheckForBossKillStreak(uint currentBossKillStreak, uint highestBossKillStreak, SocketCommandContext context, UserAccount account)
        {
            if (currentBossKillStreak > highestBossKillStreak)
            {
                account.BattleStatistics.BossStatistics.HighestBossKillStreak = account.BattleStatistics.BossStatistics.CurrentBossKillStreak;
                await context.Channel.SendMessageAsync($"Your scored a new Boss {account.BattleStatistics.BossStatistics.HighestBossKillStreak} Killingstreak!");
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