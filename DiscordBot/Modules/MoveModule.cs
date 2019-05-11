using DiscordBot.Discord;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Modules
{
    public class MoveModule : ModuleBase<SocketCommandContext>
    {
        [Command("move")]
        [Summary("Moves all online members to the channel of the game the members are playing")]
        [RequireUserPermission(GuildPermission.MoveMembers)]
        [RequireBotPermission(GuildPermission.MoveMembers)]
        public async Task MovePlayers()
        {
            var players = Context.Guild.Users;
            var voiceChannels = Context.Guild.VoiceChannels;

            foreach (var player in players)
            {
                if (player.Activity != null)
                {
                    if (player.Activity.Type == ActivityType.Playing)
                    {
                        string game = player.Activity.Name;
                        switch (game)
                        {
                            case "Dota 2":
                                await DiscordUtilities.MoveToChannel(player, voiceChannels, "Dota 2");
                                break;
                            case "Minecraft":
                                await DiscordUtilities.MoveToChannel(player, voiceChannels, "Minecraft");
                                break;
                            case "LabyMod":
                                await DiscordUtilities.MoveToChannel(player, voiceChannels, "Minecraft");
                                break;
                            case "Rocket League":
                                await DiscordUtilities.MoveToChannel(player, voiceChannels, "Rocket League");
                                break;
                            case "Overwatch":
                                await DiscordUtilities.MoveToChannel(player, voiceChannels, "Overwatch");
                                break;
                            default:
                                await DiscordUtilities.MoveToChannel(player, voiceChannels, "General");
                                break;
                        }
                    }
                }
            }

        }
    }
}
