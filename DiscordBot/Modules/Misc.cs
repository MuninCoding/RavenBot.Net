using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            await Context.Message.DeleteAsync();
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(Color.DarkBlue);
            embed.WithThumbnailUrl("https://orig00.deviantart.net/3033/f/2016/103/0/c/mercy_by_raichiyo33-d9yufl4.jpg");
            var pickEmbed = embed.Build();
            await Context.Channel.SendMessageAsync(embed: pickEmbed);
        }

        [Command("love")]
        public async Task LoveMsg()
        {
            await Context.Message.DeleteAsync();
            if (Context.Message.Author.Id == 250370033216126977)
            {
                var embed = new EmbedBuilder
                {
                    Title = "Aww i can´t believe",
                    Description = $"{Context.Message.Author.Mention} love a Very Important Person "
                };

                var Munin = embed.Build();
                await ReplyAsync(embed: Munin);
            }
            else if (Context.Message.Author.Id == 435816556530892801)
            {
                var embed = new EmbedBuilder
                {
                    Title = "Aww i can´t believe",
                    Description = $"{Context.Message.Author.Mention} love a Very Important Person "
                };
                var Lilly = embed.Build();

                await ReplyAsync(embed: Lilly);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Not Allowed");
                await Task.Delay(6000);
                await Context.Message.DeleteAsync();
            }

        }
    }
}
