using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem;
using DiscordBot.BattleSystem.Handlers;
using DiscordBot.BattleSystem.Enemies;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class FarmModule : ModuleBase<SocketCommandContext>
    {
        [Command("farm", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Battle()
        {
            await Context.Message.DeleteAsync();
            int messageCount = 1;

            //Getting Playerstats and creating creeps
            UserAccount account = UserManager.GetAccount(Context.Message.Author);
            //Getting a list of enemies form the EnemyUtilites class with the level of the user

            List<IEnemy> enemies = SpawnHandler.SpawnEnemies(account.BattleStatistics.Level, account.BattleStatistics.Damage, false);

            if (enemies.Count == 1)
            {
                await ReplyAsync($"A Wild {enemies[0].Name} appeared!");
                messageCount++;
            }
            else
            {
                await ReplyAsync($"{enemies.Count} enemies appeared!");
                await ReplyAsync($"{enemies[0].Name} is in the first Wave!");
                await ReplyAsync($"{enemies[1].Name} is in the second Wave!");
                messageCount += 3;
            }

            bool isWinner = await FarmHandler.SimulateFight(enemies, account, Context.Channel);
            bool isNewCreepWinStreak = false;
            bool isNewHighestCreepKillStreak = false;
            bool leveledUp = false;

            //Adding Xp and Rewards (?)
            if (isWinner)
            {
                
                await ReplyAsync($"{account.Name} Won this Fight and earned 20 XP for it!");
                messageCount++;

                //Increase properties
                account.BattleStatistics.CreepStatistics.CreepBattlesWon++;
                account.BattleStatistics.CreepStatistics.CurrentCreepWinStreak++;

                //Check for creep winstreak
                uint currentCreepWinStreak = account.BattleStatistics.CreepStatistics.CurrentCreepWinStreak;
                uint highestCreepWinStreak = account.BattleStatistics.CreepStatistics.HighestCreepWinStreak;
                isNewCreepWinStreak = await StatisticHandler.CheckForCreepWinstreak(currentCreepWinStreak, highestCreepWinStreak, Context, account);
                if (isNewCreepWinStreak)
                    messageCount++;

                //Check for highest creep kills
                uint currentCreepKills = account.BattleStatistics.CreepStatistics.CurrentCreepKillStreak;
                uint highestCreepKills = account.BattleStatistics.CreepStatistics.HighestCreepKillStreak;
                isNewHighestCreepKillStreak = await StatisticHandler.CheckForCreepKillStreak(currentCreepKills, highestCreepKills, Context, account);
                if (isNewHighestCreepKillStreak)
                    messageCount++;

                //Levelup check
                uint oldLevel = account.BattleStatistics.Level;
                account.BattleStatistics.Xp += 20;
                uint newLevel = account.BattleStatistics.Level;

                leveledUp = await StatisticHandler.CheckForLevelUp(oldLevel, newLevel, Context, account);
                messageCount = await ItemHandler.CheckForItemDrop(account, Context, messageCount);

                if (leveledUp)
                {
                    messageCount++;
                }
            }
            else
            {
                await ReplyAsync("Good luck next time.");
                messageCount++;
                account.BattleStatistics.CreepStatistics.CreepBattlesLost++;
                account.BattleStatistics.CreepStatistics.CurrentCreepKillStreak = 0;
                account.BattleStatistics.CreepStatistics.CurrentCreepWinStreak = 0;
            }

            account.BattleStatistics.CreepStatistics.CreepBattlesFought++;
            UserManager.SaveAccounts();
            StatisticHandler.RewriteHighscores();
            await Task.Delay(1000);
            var messages = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
            var messageList = messages.ToList();
            if (leveledUp)
            {
                messageList.RemoveAt(0);
            }
            foreach (var message in messageList)
            {
                await Task.Delay(10000);
                await message.DeleteAsync();
            }
        }
    }
}
