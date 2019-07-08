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

            bool playerWantsToFightBoss = false;

            //Getting Playerstats and creating creeps
            UserAccount account = UserManager.GetAccount(Context.Message.Author);
            //Getting a list of enemies form the EnemyUtilites class with the level of the user
            if (account.BattleStatistics.Level == 5 && !account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined)
            {
                var channel = await Context.Message.Author.GetOrCreateDMChannelAsync();
                await channel.SendMessageAsync($"A boss appeared, do you want to fight it?");
                await Task.Delay(5000);
                var dmMessages = await channel.GetMessagesAsync(1).FlattenAsync();
                foreach (var message in dmMessages)
                {

                    if (message.Content.Equals("yes"))
                    {
                        playerWantsToFightBoss = true;
                    }
                    else if (message.Content.Equals("no"))
                    {
                        playerWantsToFightBoss = false;
                        account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined = true;

                    }
                }
            }

            List<IEnemy> enemies = SpawnHandler.SpawnEnemies(account.BattleStatistics.Level, account.BattleStatistics.Damage, account.BattleStatistics.BossStatistics.BossBattleFoughtOrDeclined, playerWantsToFightBoss);

            if (!playerWantsToFightBoss)
            {
                if (enemies.Count == 1)
                {
                    await ReplyAsync("A Wild enemy appeared!");
                    messageCount++;
                }
                else
                {
                    await ReplyAsync($"{enemies.Count} enemies appeared!");
                    messageCount++;
                }
            }
            else
            {
                await ReplyAsync($"A Boss appeared. Good Luck!");
                messageCount++;
            }


            bool isWinner = true;
            bool isFighting = true;
            bool leveledUp = false;
            bool isNewCreepWinStreak = false;
            bool isNewHighesthighestCreepKillStreak = false;

            float playerHealth = account.BattleStatistics.Health;
            float playerDefense = account.BattleStatistics.Defense;
            float playerDamage = account.BattleStatistics.Damage;

            //Simulating fight
            do
            {
                //Looping through all enemies in the returned array from EnemyUtilites
                foreach (var enemy in enemies)
                {
                    //And letting all of them attack the player
                    playerHealth -= enemy.Damage - playerDefense;
                    await ReplyAsync($"Player was hit for {enemy.Damage} damage and blocked {playerDefense} damage. Player`s current Health is {playerHealth}!");
                    messageCount++;

                }
                //If playerhealth is less the 0
                if (playerHealth <= 0)
                {                                                                                                                                                                                                                                
                    await ReplyAsync($"You died!");
                    messageCount++;
                    //Set isFighting to false and exit the current iteration of the loop with continue
                    isFighting = false;
                    isWinner = false;
                    continue;
                }

                //Attack the first enemy in the enemies list
                enemies[0].Health -= playerDamage;
                //Write health of player and the currently attack creep
                await ReplyAsync($"Enemy was hit for {playerDamage} damage. Enemy`s current Health is {enemies[0].Health}");
                messageCount++;

                //If the enemies health is 0 or below
                if (enemies[0].Health <= 0)
                {
                    account.BattleStatistics.CreepStatistics.CurrentCreepKillStreak++;
                    account.BattleStatistics.CreepStatistics.AmountOfCreepsKilled++;
                    await ReplyAsync("Enemy died!");
                    messageCount++;
                    //Remove this enemy from the list
                    enemies.RemoveAt(0);
                }

                //If the enemies list is empty
                if (enemies.Count == 0)
                    isFighting = false;
                await Task.Delay(5000);

            } while (isFighting);

            //Adding Xp and Rewards (?)
            if (isWinner)
            {
                if (!playerWantsToFightBoss)
                {
                    await ReplyAsync("You Won this Fight!");
                    await ReplyAsync("You gained 10 Xp for your Win! Congrats");
                    messageCount += 2;

                    account.BattleStatistics.CreepStatistics.CreepBattlesWon++;
                    account.BattleStatistics.CreepStatistics.CurrentCreepWinStreak++;

                    var generator = new Random();
                    double random = generator.NextDouble();
                    if (random <= 0.25)
                    {
                        account.BattleStatistics.Weapons.Add(new Bat());
                        await ReplyAsync("You Found a Bat with 15 Attack Damage!");
                        account.BattleStatistics.DropStatistics.CreepDrops++;
                        messageCount++;
                    }
                    else if (random <= 0.5)
                    {
                        account.BattleStatistics.Weapons.Add(new Rock());
                        await ReplyAsync("You Found a Rock with 10 Attack Damage!");
                        account.BattleStatistics.DropStatistics.CreepDrops++;
                        messageCount++;
                    }
                    else if (random <= 0.0001)
                    {
                        account.BattleStatistics.Weapons.Add(new DivineRapier());
                        await ReplyAsync("You Found a DivineRapier with 1000 Attack Damage!");
                        account.BattleStatistics.DropStatistics.CreepDrops++;
                        messageCount++;
                    }

                    uint oldLevel = account.BattleStatistics.Level;
                    account.BattleStatistics.Xp += 10;
                    uint newLevel = account.BattleStatistics.Level;
                    leveledUp = await StatisticHandler.CheckForLevelUp(oldLevel, newLevel, Context, account);
                    if (leveledUp)
                        messageCount++;

                    uint currentCreepWinStreak = account.BattleStatistics.CreepStatistics.CurrentCreepWinStreak;
                    uint HighestCreepWinStreak = account.BattleStatistics.CreepStatistics.HighestCreepWinStreak;
                    isNewCreepWinStreak = await StatisticHandler.CheckForCreepWinstreak(currentCreepWinStreak, HighestCreepWinStreak, Context, account);
                    if (isNewCreepWinStreak)
                        messageCount++;

                    uint currentCreepKills = account.BattleStatistics.CreepStatistics.CurrentCreepKillStreak;
                    uint highestCreepKills = account.BattleStatistics.CreepStatistics.HighestCreepKillStreak;
                    isNewHighesthighestCreepKillStreak = await StatisticHandler.CheckForCreepKillStreak(currentCreepKills, highestCreepKills, Context, account);
                    if (isNewHighesthighestCreepKillStreak)
                        messageCount++;

                }
                else
                {
                    //TODO Reward player for Boss Win
                }
            }
            else
            {
                if (!playerWantsToFightBoss)
                {
                    await ReplyAsync("Good luck next time!");
                    messageCount++;
                    account.BattleStatistics.BossStatistics.BossBattlesLost++;
                    account.BattleStatistics.BossStatistics.CurrentBossWinStreak = 0;
                    account.BattleStatistics.BossStatistics.CurrentBossKillStreak = 0;
                }
                else
                {
                    //TODO Inform the player about the boss loss
                }
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
