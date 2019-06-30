using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using DiscordBot.BattleSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class PvPModule : ModuleBase<SocketCommandContext>
    {
        [Command("fight", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Fight(SocketGuildUser user)
        {
            await Context.Message.DeleteAsync();
            UserAccount authorAccount = UserManager.GetAccount(Context.Message.Author);
            UserAccount socketUserAccount = UserManager.GetAccount(user);
            authorAccount.BattleStatistics.PvPChallengesRequests++;

            var channel = await user.GetOrCreateDMChannelAsync();
            await channel.SendMessageAsync($"{Context.Message.Author} want to fight with you!Do you want to accept the challenge?");
            await Task.Delay(5000);
            var messages = await channel.GetMessagesAsync(1).FlattenAsync();
            int messageCount = 1;
            foreach (var message in messages)
            {
                
                if (message.Content.Equals("yes"))
                {
                    authorAccount.BattleStatistics.PvPBattlesFought++;
                    socketUserAccount.BattleStatistics.PvPBattlesFought++;
                    socketUserAccount.BattleStatistics.PvPBattlesAccepted++;
                    await ReplyAsync("Battle was accepted");
                    await channel.SendMessageAsync("Battle was accepted");

                    int player1Health = authorAccount.BattleStatistics.Health;
                    int player1Defense = authorAccount.BattleStatistics.Defense;
                    int player1Damage = authorAccount.BattleStatistics.Damage;

                    int player2Health = socketUserAccount.BattleStatistics.Health;
                    int player2Defense = socketUserAccount.BattleStatistics.Defense;
                    int player2Damage = socketUserAccount.BattleStatistics.Damage;
                   
                    bool authorIsWinner = true;
                    bool isFighting = true;
                    bool leveledUp = false;

                    //StartFight();
                    await ReplyAsync("Battle Beginns");
                    messageCount++;
                    await channel.SendMessageAsync("Battle Beginns");
                    do
                    {
                        await Task.Delay(3000);
                        player1Health -= player2Damage - player1Defense;
                        if (player1Health <= 0)
                        {
                            await ReplyAsync("You died!");
                            messageCount++;
                            isFighting = false;
                            authorIsWinner = false;
                            continue;
                        }

                        player2Health -= player1Damage - player2Defense;
                        await ReplyAsync($"{Context.Message.Author}´s current Health is {player1Health}!");
                        await ReplyAsync($"{user}´s current Health is {player2Health}!");
                        messageCount += 2;
                        isFighting = !(player1Health <= 0 || player2Health <= 0);

                    } while (isFighting);

                    if (authorIsWinner)
                    {
                        await ReplyAsync("You won this Fight");
                        messageCount++;
                        await channel.SendMessageAsync("You lost this Fight sorry!");

                        authorAccount.BattleStatistics.PvPBattlesWon++;
                        authorAccount.BattleStatistics.CurrentPvPWinStreak++;
                        socketUserAccount.BattleStatistics.PvPBattlesLost++;
                        socketUserAccount.BattleStatistics.CurrentPvPWinStreak = 0;

                        uint oldLevel = authorAccount.BattleStatistics.Level;
                        authorAccount.BattleStatistics.BattleXp += 50;
                        uint newLevel = authorAccount.BattleStatistics.Level;


                        leveledUp = await StatisticUtilites.CheckForLevelUp(oldLevel, newLevel, Context, authorAccount);

                        if (leveledUp)
                            messageCount++;
                    }
                    else
                    {
                        await channel.SendMessageAsync("You won this Fight!");
                        await ReplyAsync("You lost Sorry!");
                        messageCount++;

                        socketUserAccount.BattleStatistics.PvPBattlesWon++;
                        socketUserAccount.BattleStatistics.CurrentPvPWinStreak++;
                        authorAccount.BattleStatistics.PvPBattlesLost++;
                        authorAccount.BattleStatistics.CurrentPvPWinStreak = 0;

                        uint oldLevel = socketUserAccount.BattleStatistics.Level;
                        socketUserAccount.BattleStatistics.BattleXp += 50;
                        uint newLevel = socketUserAccount.BattleStatistics.Level;

                        leveledUp = await StatisticUtilites.CheckForLevelUp(oldLevel, newLevel, Context, socketUserAccount);

                        if (leveledUp)
                            messageCount++;
                    }

                    UserManager.SaveAccounts();
                    var message1 = await Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
                    var messageList = message1.ToList();
                    if (leveledUp)
                    {
                        messageList.RemoveAt(0);
                    }
                    foreach (var text in messageList)
                    {
                        await Task.Delay(60000);
                        await text.DeleteAsync();
                    }

                }
                else
                {
                    var botMsg = await ReplyAsync("Battle was declined");
                    await Task.Delay(30000);
                    await botMsg.DeleteAsync();
                    await channel.SendMessageAsync("Battle was declined");
                    socketUserAccount.BattleStatistics.PvPBattlesDeclined++;
                    UserManager.SaveAccounts();
                }
            }
        }
    }
}
