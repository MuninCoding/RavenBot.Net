using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class ModModule : ModuleBase<SocketCommandContext>
    {
        [Command("warn")]

        public async Task WarnUser(IGuildUser user, [Remainder]string reason = "No reason provided")
        {
            var userAccount = UserManager.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserManager.SaveAccounts();

            if (userAccount.NumberOfWarnings == 5)
            {
                var channel = await user.GetOrCreateDMChannelAsync();
                await channel.SendMessageAsync($"This was your last Chance you gonna be banned for" + reason);
                await user.Guild.AddBanAsync(user, 5);
            }
            else if (userAccount.NumberOfWarnings == 4)
            {
                var channel = await user.GetOrCreateDMChannelAsync();
                await channel.SendMessageAsync($"This is your {userAccount.NumberOfWarnings} Warning! for" + reason + " Next time you will be banned!");
                await user.KickAsync();
            }
            else if (userAccount.NumberOfWarnings >= 1)
            {
                var channel = await user.GetOrCreateDMChannelAsync();
                await channel.SendMessageAsync($"This is your {userAccount.NumberOfWarnings} Warning! for" + reason + " be careful!");
            }
        } 

        [Command("mute")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task MuteUser(IGuildUser userAccount, bool muteState, [Remainder]string reason = "No reason provided")
        {
            if (!userAccount.GuildPermissions.Administrator)
            {
                await userAccount.ModifyAsync(x => x.Mute = muteState);
                if (muteState)
                {
                    await Context.Channel.SendMessageAsync($"The user {userAccount} was muted for the following reason:\n{reason} from:{Context.Message.Author}");
                    await Context.Guild.GetTextChannel(525136686431207425).SendMessageAsync($"The user {userAccount} was muted for the following reason:\n{reason} from:{Context.Message.Author}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"The user {userAccount.Username} was unmuted from:{Context.Message.Author}");
                    await Context.Guild.GetTextChannel(525136686431207425).SendMessageAsync($"The user {userAccount.Username} was unmuted from:{Context.Message.Author}");
                }
            }
            else
            {
                await ReplyAsync("You can not mute or unmute an administrator");
            }
        }


        [Command("kick")]
        [Summary("Kicks a user from the server")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser userAccount,[Remainder]string reason = "No reason provided")
        {
            var user = Context.User as IGuildUser;
            if (user.GuildPermissions.KickMembers)
            {
                await userAccount.KickAsync(reason);
                await Context.Channel.SendMessageAsync($"The user {userAccount} has been kicked, for {reason} from:{Context.Message.Author}");
                await Context.Guild.GetTextChannel(525136686431207425).SendMessageAsync($"The user {userAccount} was kicked for the following reason:\n{reason} from:{Context.Message.Author}");
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
            await Context.Channel.SendMessageAsync($"The user {userAccount} has been Banned, for {reason} from:{Context.Message.Author}");
            await Context.Guild.GetTextChannel(525136686431207425).SendMessageAsync($"The user {userAccount} was Banned for the following reason:\n{reason} from:{Context.Message.Author}");
        }

    }
}
