using Discord;
using Discord.Commands;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
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
            await ReplyAsync("Gambling started - Please enter a Number between 1 and 100!");
            await Task.Delay(timeInMs);

            //Getting the messages sent to the channel
            await Context.Channel.GetMessageAsync();
            var messages = await Context.Channel.GetMessagesAsync().FlattenAsync();
            //Add a bit of delay because the async call can take time and we may get unwanted messages in our collection
            messages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalMilliseconds <= (timeInMs - delayInMs));

            //Generate the random number
            int maxAmount = 100;
            var generator = new Random();
            int randomNumber = generator.Next(1, maxAmount);
            await ReplyAsync($"The random number between 1 and {maxAmount} is: {randomNumber}!");

            //Check for winner
            string winnerName = "No Entry";
            int closestDifference = 100;
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
                    
                    
                }
            }
            await ReplyAsync($"The winner is {winnerName} with a difference of {closestDifference} from {closestNumber} to {randomNumber}.");
            

        }

    }
}
