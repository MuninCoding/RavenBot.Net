using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem;
using DiscordBot.BattleSystem.Utilities;
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
            await ReplyAsync("Starting fight");
            int messageCount = 1;

            //Getting Playerstats and creating creeps
            UserAccount account = UserManager.GetAccount(Context.Message.Author);
            //Getting a list of enemies form the EnemyUtilites class with the level of the user
            List<IEnemy> enemies = EnemyUtilites.SpawnEnemies(account.BattleStatistics.Level, account.BattleStatistics.Damage);
            if(enemies.Count == 1)
            {
                await ReplyAsync("A Wild enemy appeared!");
            }
            else
            {
                await ReplyAsync($"{enemies.Count} enemies appeared!");

            }
            messageCount++;



            bool isWinner = true;
            bool isFighting = true;
            bool leveledUp = false;

            int playerHealth = account.BattleStatistics.Health;
            int playerDefense = account.BattleStatistics.Defense;
            int playerDamage = account.BattleStatistics.Damage;

            //Simulating fight
            do
            {
           
                //Looping through all enemies in the returned array from EnemyUtilites
                foreach (var enemy in enemies)
                {
                    //And letting all of them attack the player
                    playerHealth -= enemy.Damage - playerDefense;

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
                await ReplyAsync($"Player`s current Health is {playerHealth}!");
                await ReplyAsync($"Enemy`s current Health is {enemies[0].Health}");
                messageCount += 2;

                //If the enemies health is 0 or below
                if (enemies[0].Health <= 0)
                {
                    await ReplyAsync("Enemy died!");
                    messageCount++;
                    //Remove this enemy from the list
                    enemies.RemoveAt(0);
                }

                //If the enemies list is empty
                if (enemies.Count == 0)
                    isFighting = false;

                    await Task.Delay(2000);

            } while (isFighting);

            //Adding Xp and Rewards (?)
            if (isWinner)
            {
                await ReplyAsync("You Won this Fight!");
                await ReplyAsync("You gained 10 Xp for your Win! Congrats");
                messageCount += 2;

                var generator = new Random();
                double random = generator.NextDouble();
                account.BattleStatistics.CreepBattlesWon++;
                if (random <= 0.25)
                {
                    account.BattleStatistics.Weapons.Add(new Bat());
                    await ReplyAsync("You Found a Bat with 15 Attack Damage!");
                    messageCount++;
                }
                else if (random <= 0.2)
                {
                    account.BattleStatistics.Weapons.Add(new Rock());
                    await ReplyAsync("You Found a Rock with 10 Attack Damage!");
                    messageCount++;
                }
                else if (random <= 0.01)
                {
                    account.BattleStatistics.Weapons.Add(new DivineRapier());
                    await ReplyAsync("You Found a DivineRapier with 1000 Attack Damage!");
                    messageCount++;
                }

                uint oldLevel = account.BattleStatistics.Level;
                account.BattleStatistics.BattleXp += 10;
                uint newLevel = account.BattleStatistics.Level;
                leveledUp = await StatisticUtilites.CheckForLevelUp(oldLevel, newLevel, Context, account);

                if (leveledUp)
                    messageCount++;

            }
            else
            {
                await ReplyAsync("Good luck next time!");
                messageCount++;
                account.BattleStatistics.CreepBattlesLost++;
            }
            account.BattleStatistics.CreepBattlesFought++;
            UserManager.SaveAccounts();
            await Task.Delay(1000);
            var messages = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
            var messageList = messages.ToList();
            if (leveledUp)
            {
                messageList.RemoveAt(0);
            }
            foreach (var message in messageList)
            {
                await message.DeleteAsync();
            }
        }
    }
}
