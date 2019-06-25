using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Love : ModuleBase<SocketCommandContext>
    {
        [Command("love")]
        public async Task LoveMsg()
        {
            if (Context.Message.Author.Id == 250370033216126977)
            {
                var embed = new EmbedBuilder
                {
                    Title = "Aww i can´t believe",
                    Description = "Munin love a Very Important Person "
                };
                // Or with methods
                //TODO UPDATE
               /* embed.WithColor(Color.Red)
                     .WithTitle("Munin is in Love")
                     .AddField("Help", "?help - Shows bot information with all available commands")
                     .AddField("Share Rank", "?sharerank {user} - Shares your rank with the specified user")
                     .AddField("Clear", "?clear - Clear the chat in the Channel there you write the Message")
                     .AddField("Mute", "?mute<@playername><MuteState> - Mute player for Voicechannel (MuteState = True|False)")
                     .AddField("Kick", "?kick<@playername><Reason> - Kick a player from Server with a specified reason")
                     .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                     .WithCurrentTimestamp();*/

                var Munin = embed.Build();

                await ReplyAsync(embed: Munin);
            }
            else if (Context.Message.Author.Id == 435816556530892801)
            {
                var embed = new EmbedBuilder
                {
                    Title = "Aww i can´t believe",
                    Description = "Lu love a Very Important Person "
                };
                var Lilly = embed.Build();

                await ReplyAsync(embed: Lilly);
            }
        }
    }
}
