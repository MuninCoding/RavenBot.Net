using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    static class DiscordUtilities
    {
        public static async Task MoveToChannel(SocketGuildUser player, IReadOnlyCollection<SocketVoiceChannel> voiceChannels, string nameOfChannel)
        {
            foreach (var channel in voiceChannels)
            {
                if (channel.Name == nameOfChannel)
                {
                    await player.ModifyAsync(x => x.Channel = channel);
                }
            }
        }
    }
}
