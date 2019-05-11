using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Prints an help embed")]
        public async Task SendHelpEmbed()
        {
            {
                var embed = new EmbedBuilder
                {
                    Title = "Help Window",
                    Description = "Displays all commands"
                };
                // Or with methods
                //TODO UPDATE
                embed.WithColor(Color.Blue)
                     .WithTitle("Bot Help")
                     .WithDescription("Below is a list with all currently available commands for Ravenplays Guardian Bot")
                     .AddField("Help", "?help - Shows bot information with all available commands")
                     .AddField("Share Rank", "?sharerank {user} - Shares your rank with the specified user")
                     .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                     .WithCurrentTimestamp();

                var info = embed.Build();

                await ReplyAsync(embed: info);
            }
        }
    }
}
