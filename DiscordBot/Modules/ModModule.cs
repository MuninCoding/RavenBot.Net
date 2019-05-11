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
    public class ModModule : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [Summary("Kicks a user from the server")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser userAccount,[Remainder]string reason = "No reason provided")
        {
            var user = Context.User as IGuildUser;
            if (user.GuildPermissions.KickMembers)
            {
                await userAccount.KickAsync(reason);
                await Context.Channel.SendMessageAsync($"The user {userAccount} has been kicked, for {reason}");
            }
            else
            {
                await Context.Channel.SendMessageAsync("No permissions for kicking a user.");
            }
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task BanUser(IGuildUser userAccount, [Remainder]string reason = "No reason provided")
        {
            await Context.Guild.AddBanAsync(userAccount);
            
            //
            //TODO Send info about Ban
        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task MuteUser(IGuildUser user, bool muteState, [Remainder]string reason = "No reason provided")
        {
            if (!user.GuildPermissions.Administrator)
            {
                await user.ModifyAsync(x => x.Mute = muteState);
                if (muteState)
                {
                    await Context.Guild.GetTextChannel(525136686431207425).SendMessageAsync($"The user {user.Username} was muted for the following reason:\n{reason}");
                }
                else
                {
                    //TODO Send info about unmuting
                    //await Context.Guild.GetTextChannel(id of the channel).SendMessageAsync(string to send)
                }
            }
            else
            {
                await ReplyAsync("You can not mute or unmute an administrator");
            }
        }
    }
}
