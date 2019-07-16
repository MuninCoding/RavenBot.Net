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
    class FarmHandler
    {
        internal static async Task<(bool isWinner, uint messageCount)> SimulateFight(List<IEnemy> enemies, UserAccount account, IMessageChannel channel, uint messageCount)
        {
            bool isFighting = true;

            float playerHealth = account.BattleStatistics.BaseHealth;
            float playerDefense = account.BattleStatistics.Defense;
            float playerDamage = account.BattleStatistics.Damage;

            if (playerHealth == 0)
            {

            }
            //Simulating fight
            do
            {
                //Looping through all enemies in the returned array from EnemyUtilites
                foreach (var enemy in enemies)
                {
                    //And letting all of them attack the player
                    playerHealth -= enemy.Damage - playerDefense;

                    await channel.SendMessageAsync($"{account.Name} was hit for {enemy.Damage} damage and blocked {playerDefense} damage. {account.Name}`s current Health is {playerHealth}!");
                    messageCount++;
                    UserManager.SaveAccounts();
                }

                //If playerhealth is less the 0
                if (playerHealth <= 0)
                {
                    await channel.SendMessageAsync($"{enemies[0].Name} killed you!");
                    messageCount++;

                    return (false, messageCount);
                }

                //Attack the first enemy in the enemies list
                enemies[0].Health -= playerDamage;

                //Write health of player and the currently attack creep
                await channel.SendMessageAsync($"{enemies[0].Name} was hit for {playerDamage} damage. {enemies[0].Name} current Health is {enemies[0].Health}");
                messageCount++;

                //If the enemies health is 0 or below
                if (enemies[0].Health <= 0)
                {
                    account.BattleStatistics.CreepStatistics.CurrentCreepKillStreak++;
                    account.BattleStatistics.CreepStatistics.AmountOfCreepsKilled++;
                    account.BattleStatistics.Gold += enemies[0].GettingGold;
                    account.BattleStatistics.Xp += enemies[0].GettingXp;

                    await channel.SendMessageAsync($"{enemies[0].Name} died! you get {enemies[0].GettingGold} Gold and {enemies[0].GettingXp} Xp for it!");
                    messageCount++;

                    //Remove this enemy from the list
                    enemies.RemoveAt(0);
                }

                //If the enemies list is empty
                if (enemies.Count == 0)
                    isFighting = false;

                await Task.Delay(5000);

            } while (isFighting);

            return (true, messageCount);
        }
    }
}
