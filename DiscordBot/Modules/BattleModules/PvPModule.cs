using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using DiscordBot.BattleSystem.Handlers;
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
            authorAccount.BattleStatistics.PvpStatistics.PvPChallengesRequests++;

            var channel = await user.GetOrCreateDMChannelAsync();
            await channel.SendMessageAsync($"{Context.Message.Author} want to fight with you!Do you want to accept the challenge?");
            await Task.Delay(5000);
            var messages = await channel.GetMessagesAsync(1).FlattenAsync();
            uint messageCount = 1;
            foreach (var message in messages)
            {
                
                if (message.Content.Equals("yes"))
                {
                    authorAccount.BattleStatistics.PvpStatistics.PvPBattlesFought++;
                    socketUserAccount.BattleStatistics.PvpStatistics.PvPBattlesFought++;
                    socketUserAccount.BattleStatistics.PvpStatistics.PvPBattlesAccepted++;
                    await ReplyAsync("Battle was accepted");
                    await channel.SendMessageAsync("Battle was accepted");

                    float player1Health = authorAccount.BattleStatistics.CurrentHealth;
                    float player1Defense = authorAccount.BattleStatistics.Defense;
                    float player1Damage = authorAccount.BattleStatistics.Damage;

                    float player2Health = socketUserAccount.BattleStatistics.CurrentHealth;
                    float player2Defense = socketUserAccount.BattleStatistics.Defense;
                    float player2Damage = socketUserAccount.BattleStatistics.Damage;
                   
                    bool authorIsWinner = true;
                    bool isFighting = true;
                    bool leveledUp = false;
                    bool isNewKillstreak = false;
                    bool isNewWinStreak = false;


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
                        await ReplyAsync($"{authorAccount}´s current Health is {player1Health}!");
                        await ReplyAsync($"{socketUserAccount}´s current Health is {player2Health}!");
                        messageCount += 2;
                        isFighting = !(player1Health <= 0 || player2Health <= 0);

                    } while (isFighting);

                    if (authorIsWinner)
                    {
                        await ReplyAsync("You won this Fight");
                        messageCount++;
                        await channel.SendMessageAsync("You lost this Fight sorry!");

                        authorAccount.BattleStatistics.PvpStatistics.AmountOfPlayersKilled++;
                        authorAccount.BattleStatistics.PvpStatistics.PvPBattlesWon++; 
                        socketUserAccount.BattleStatistics.PvpStatistics.PvPBattlesLost++;
                        socketUserAccount.BattleStatistics.PvpStatistics.CurrentPvpWinStreak = 0;
                        socketUserAccount.BattleStatistics.PvpStatistics.CurrentPvpKillStreak = 0;

                        uint oldLevel = authorAccount.BattleStatistics.Level;
                        authorAccount.BattleStatistics.Xp += 50;
                        uint newLevel = authorAccount.BattleStatistics.Level;

                        var levelResult = await StatisticHandler.CheckForLevelUp(oldLevel, newLevel, Context, authorAccount, messageCount);
                        leveledUp = levelResult.leveledUp;
                        messageCount = levelResult.messageCount;

                        uint currentPlayerKillStreak = authorAccount.BattleStatistics.PvpStatistics.CurrentPvpKillStreak;
                        authorAccount.BattleStatistics.PvpStatistics.CurrentPvpKillStreak++;
                        uint highestPlayerKillStreak = authorAccount.BattleStatistics.PvpStatistics.HighestPvpKillStreak;
                        isNewKillstreak = await StatisticHandler.CheckForPlayerKills(currentPlayerKillStreak, highestPlayerKillStreak, Context, authorAccount);
                        if (isNewKillstreak)
                            messageCount++;

                        uint currentWinStreak = authorAccount.BattleStatistics.PvpStatistics.CurrentPvpWinStreak;
                        authorAccount.BattleStatistics.PvpStatistics.CurrentPvpWinStreak++;
                        uint highestWinStreak = authorAccount.BattleStatistics.PvpStatistics.HighestPvpWinStreak;
                        isNewWinStreak = await StatisticHandler.CheckForPvpWinstreak(currentWinStreak, highestWinStreak, Context, authorAccount);
                        if (isNewWinStreak)
                            messageCount++;
                    }
                    else
                    {
                        await channel.SendMessageAsync("You won this Fight!");
                        await ReplyAsync("You lost Sorry!");
                        messageCount++;

                        socketUserAccount.BattleStatistics.PvpStatistics.AmountOfPlayersKilled++;
                        socketUserAccount.BattleStatistics.PvpStatistics.PvPBattlesWon++;
                        authorAccount.BattleStatistics.PvpStatistics.PvPBattlesLost++;
                        authorAccount.BattleStatistics.PvpStatistics.CurrentPvpWinStreak = 0;
                        authorAccount.BattleStatistics.PvpStatistics.CurrentPvpKillStreak = 0;

                        uint oldLevel = socketUserAccount.BattleStatistics.Level;
                        socketUserAccount.BattleStatistics.Xp += 50;
                        uint newLevel = socketUserAccount.BattleStatistics.Level;

                        var levelResult = await StatisticHandler.CheckForLevelUp(oldLevel, newLevel, Context, authorAccount, messageCount);
                        leveledUp = levelResult.leveledUp;
                        messageCount = levelResult.messageCount;

                        uint currentPlayerKillStreak = socketUserAccount.BattleStatistics.PvpStatistics.CurrentPvpKillStreak;
                        socketUserAccount.BattleStatistics.PvpStatistics.CurrentPvpKillStreak++;
                        uint highestPlayerKillStreak = socketUserAccount.BattleStatistics.PvpStatistics.HighestPvpKillStreak;
                        isNewKillstreak = await StatisticHandler.CheckForCreepKillStreak(currentPlayerKillStreak, highestPlayerKillStreak, Context, socketUserAccount);
                        if (isNewKillstreak)
                            messageCount++;
                        
                        uint currentWinStreak = socketUserAccount.BattleStatistics.PvpStatistics.CurrentPvpWinStreak;
                        socketUserAccount.BattleStatistics.PvpStatistics.CurrentPvpWinStreak++;
                        uint highestWinStreak = socketUserAccount.BattleStatistics.PvpStatistics.HighestPvpWinStreak;
                        isNewWinStreak = await StatisticHandler.CheckForPvpWinstreak(currentWinStreak, highestWinStreak, Context, socketUserAccount);
                        if (isNewWinStreak)
                            messageCount++;
                    }

                    UserManager.SaveAccounts();
                    var message1 = await Context.Channel.GetMessagesAsync((int)messageCount).FlattenAsync();
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
                    socketUserAccount.BattleStatistics.PvpStatistics.PvPBattlesDeclined++;
                    UserManager.SaveAccounts();
                }
            }
        }
    }
}
