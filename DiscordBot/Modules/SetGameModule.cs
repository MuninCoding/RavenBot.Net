using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class SetGameModule : ModuleBase<SocketCommandContext>
    {
        [Command("setgame")]
        

        public async Task SetGame([Remainder] string game)
        {
            if(Context.Message.Author.Id == 250370033216126977)
            {
                await Context.Client.SetGameAsync(game);
                await Context.Message.DeleteAsync();
            }
            else
            {
                await Context.Channel.SendMessageAsync("Not Allowed");
                await Context.Message.DeleteAsync();
            }

        }
    }
}
