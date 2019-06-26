using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RandomNumberModule : ModuleBase<SocketCommandContext>
{
    [Command("random")]
    [Summary("Generates a random number")]
     public async Task GenerateRandomNumber(int maxAmount = 100)
     {
         var generator = new Random();
         int randomNumber = generator.Next(1, maxAmount);
         await Task.Delay(2000);
         await ReplyAsync($"The random number between 1 and {maxAmount} is: {randomNumber}!");

     }
}
