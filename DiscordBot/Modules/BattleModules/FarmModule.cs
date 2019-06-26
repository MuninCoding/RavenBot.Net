using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem;
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
            //Getting Playerstats and creating creeps
            UserAccount account = UserManager.GetAccount(Context.Message.Author);
            Creep enemy = new Creep();

            int playerHealth = account.BattleStatistics.Health;
            int playerDefense = account.BattleStatistics.Defense;
            int playerDamage = account.BattleStatistics.Damage;
            bool isWinner = true;
            bool isFighting = true;

            int enemyHealth = enemy.Health;
            int enemyDamage = enemy.Damage;

            await ReplyAsync("A Wild enemy appeared!");

            //Simulating fight
            do
            {
                await Task.Delay(2000);
                playerHealth -= enemyDamage - playerDefense;
                if (playerHealth <= 0)
                {
                    await ReplyAsync($"You died!");
                    isFighting = false;
                    isWinner = false;
                    continue;
                }
                enemyHealth -= playerDamage;
                await ReplyAsync($"Player`s current Health is {playerHealth}!");
                await ReplyAsync($"Enemy`s current Health is {enemyHealth}");
                isFighting = !(enemyHealth <= 0 || playerHealth <= 0);

            } while (isFighting);

            //Adding Xp and Rewards (?)
            if (isWinner)
            {
                uint oldLevel = account.BattleStatistics.Level;

                account.BattleStatistics.Xp += 10;
                uint newLevel = account.BattleStatistics.Level;

                await ReplyAsync("You Won this Fight!");
                await ReplyAsync("You gained 10 Xp for your Win! Congrats");

                var generator = new Random();
                double random = generator.NextDouble();
                account.BattleStatistics.CreepBattlesWon++;
                if (random >= 0.5)
                {
                    account.BattleStatistics.Weapons.Add(new Bat());
                    await ReplyAsync("You Found a Bat with 15 Attack Damage!");
                }
                else if (random >= 0.2)
                {
                    account.BattleStatistics.Weapons.Add(new Rock());
                    await ReplyAsync("You Found a Rock with 10 Attack Damage!");
                }

                if (oldLevel < newLevel)
                {
                    account.BattleStatistics.BattlePoints++;
                    var embed = new EmbedBuilder();
                    embed.WithColor(67, 160, 71);
                    embed.WithTitle("LEVEL UP!");
                    embed.WithDescription(Context.Message.Author.Username + " just leveled up!");
                    embed.AddField("LEVEL", newLevel);
                    embed.AddField("XP", account.BattleStatistics.Xp);
                    var info = embed.Build();
                    await ReplyAsync(embed: info);
                }
            }
            else
            {
                await ReplyAsync("Good luck next time!");
                account.BattleStatistics.CreepBattlesLost++;
            }
            account.BattleStatistics.CreepBattlesFought++;
            UserManager.SaveAccounts();
            await Task.Delay(60000);
            await Context.Message.DeleteAsync();
        }
    }
}
