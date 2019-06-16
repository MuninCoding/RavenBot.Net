using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem;
using DiscordBot.BattleSystem.Enemys;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class BattleModule : ModuleBase<SocketCommandContext>
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
            bool isWinner = false;
            bool isDraw = false;
            bool isFighting = true;

            int enemyHealth = enemy.Health;
            int enemyDamage = enemy.Damage;

            await ReplyAsync("A Wild enemy appeared!");

            //Simulating fight
            do
            {
                await Task.Delay(2000);
                playerHealth -= enemyDamage - playerDefense;
                enemyHealth -= playerDamage;
                await ReplyAsync($"Player`s current Health is {playerHealth}!");
                await ReplyAsync($"Enemy`s current Health is {enemyHealth}");
                isFighting = !(enemyHealth <= 0 || playerHealth <= 0);

            } while (isFighting);

            //Checking for winner
            if (enemyHealth <= 0 && playerHealth <= 0)
            {
                isDraw = true;
            }
            else if(enemyHealth <= 0 && playerHealth > 0)
            {
                isWinner = true;
            }

            //Adding Xp and Rewards (?)
            if (isWinner)
            {
                account.BattleStatistics.Xp += 10;
                await ReplyAsync("You Won this Fight!");
                await ReplyAsync("You gained 10 Xp for your Win! Congrats");

                var generator = new Random();
                double random = generator.NextDouble();
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

            }
            else if (isDraw)
            {
                account.BattleStatistics.Xp += 5;
                await ReplyAsync("It was a Draw Good Luck next time!");
                await ReplyAsync("But you gained 5 Xp for your try!");
            }
            UserManager.SaveAccounts();
        }

        [Command("equipweapon")]
        [Alias("ew")]
        public async Task EquipWeapon(string weaponName)
        {
            var context = Context;
            await BattleUtilities.EquipWeapon(weaponName, context);
        }

        [Command("equipshield")]
        [Alias("es")]
        public async Task EquipShield(string shieldName)
        {
            var context = Context;
            await BattleUtilities.EquipShield(shieldName, context);
        }

        [Command("equiparmor")]
        [Alias("ea")]
        public async Task EquipArmor(string armorName)
        {
            var context = Context;
            await BattleUtilities.EquipArmor(armorName, context);
        }

        [Command("battlestats")]
        [Alias("bs")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task BattleStats(SocketGuildUser user = null)
        {
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
            }

            var embed = new EmbedBuilder
            {
                Title = "Help Window",
                Description = "Displays all commands"
            };

            // Or with methods
            //TODO UPDATE
            embed.WithColor(Color.Blue)
                 .WithTitle("Battle Stats")
                 .WithDescription("Get information of your battle stats")
                 .AddField("Health", account.BattleStatistics.Health.ToString(), true)
                 .AddField("Damage", account.BattleStatistics.Damage.ToString(), true)
                 .AddField("Defense", account.BattleStatistics.Defense.ToString(), true)
                 .AddField("Level", account.BattleStatistics.Level.ToString(), true)
                 .AddField("XP", account.BattleStatistics.Xp.ToString(), true)
                 .AddField("Skill Points", account.BattleStatistics.SkillPoints.ToString(), true)
                 .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                 .WithCurrentTimestamp();

            var info = embed.Build();

            await ReplyAsync(embed: info);

        }

    }
}
