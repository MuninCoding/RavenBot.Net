﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.MiniGamesModule
{
    public class RaffleModule : ModuleBase<SocketCommandContext>
    {
        [Command("raffle")]
        [Summary("Starts a raffle where the user closest to a random number is the winner.")]
        public async Task StartRaffle()
        {
            int timeInMs = 10000;
            int delayInMs = 500;

            //Start the gamble and wait for x seconds
            await ReplyAsync("Gambling started - Please enter a Number between 1 and 50!");
            await Task.Delay(timeInMs);

            //Getting the messages sent to the channel
            await Context.Channel.GetMessageAsync(593868788508786713);
            var messages = await Context.Channel.GetMessagesAsync().FlattenAsync();
            //Add a bit of delay because the async call can take time and we may get unwanted messages in our collection
            messages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalMilliseconds <= (timeInMs - delayInMs));
            
            //Generate the random number
            int maxAmount = 50;
            var generator = new Random();
            int randomNumber = generator.Next(1, maxAmount);
            await ReplyAsync($"The random number between 1 and {maxAmount} is: {randomNumber}!");

            //Check for winner
            string winnerName = "No Entry";
            int closestDifference = 50;
            int closestNumber = 0;
            foreach (var message in messages)
            {
                int playerNumber;
                int.TryParse(message.Content, out playerNumber);
                int playerDifference = randomNumber - playerNumber;
                playerDifference = Math.Abs(playerDifference);
                if (playerDifference < closestDifference)
                {
                    closestDifference = playerDifference;
                    closestNumber = playerNumber;
                    winnerName = message.Author.Username;
                    SocketUser user = message.Author as SocketUser;
                    var winner = UserManager.GetAccount(user);
                    winner.XP += 10;
                    UserManager.SaveAccounts();
                }
            }
            await ReplyAsync($"The winner is {winnerName} with a difference of {closestDifference} from {closestNumber} to {randomNumber} ,you gained 10 XP for your Win.");
            
        }
    }
}
