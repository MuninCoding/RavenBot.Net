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
        /*
        [Command("setactivity")]
        [Alias("sa")]
        public async Task SetActivity([Remainder] IActivity activity)
        {
             await Context.Client.SetActivityAsync(activity);
        }
        */
        [Command("setgame")]
        [Alias("sg")]
        public async Task SetGame([Remainder] string game)
        {
            if (Context.Message.Author.Id == 250370033216126977)
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

        [Command("setstatus")]
        [Alias("ss")]
        public async Task SetStatus([Remainder]string status)
        {
            await Context.Message.DeleteAsync();
            if (status.Equals("afk"))
            {
                await Context.Client.SetStatusAsync(UserStatus.AFK);
            }
            else if(status.Equals("dnd"))
            {
                await Context.Client.SetStatusAsync(UserStatus.DoNotDisturb);
            }
            else if (status.Equals("invisible"))
            {
                await Context.Client.SetStatusAsync(UserStatus.Invisible);
            }
            else
            {
                await Context.Client.SetStatusAsync(UserStatus.Online);
            }
        }

        [Command("clear")]
        [Summary("Clear messages from a channel. Ammount can be specified as argument. Defaults to 10, Maximum is 100")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task DeleteMessageAsync(int amount = 10, bool pinned = false)
        {
            const int delay = 5000;
            if (amount <= 0)
            {
                var botMsg = await ReplyAsync("The amount of messages to remove must be positive.");
                await Task.Delay(delay);
                await botMsg.DeleteAsync();
                return;
            }

            if (amount <= 100)
            {
                var messages = await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, amount).FlattenAsync();
                var filteredMessages = messages;

                if (pinned)
                {
                    filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);
                }
                else
                {
                    filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14 && !x.IsPinned);
                }

                var count = filteredMessages.Count();

                if (count == 0)
                {
                    var botMsg = await ReplyAsync("Nothing to delete.");
                    await Task.Delay(delay);
                    await botMsg.DeleteAsync();
                }
                else
                {
                    await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
                    var botMsg = await ReplyAsync($"Done. Removed {count} {(count > 1 ? "messages" : "message")}.");
                    await Task.Delay(delay);
                    await Context.Message.DeleteAsync();
                    await botMsg.DeleteAsync();

                }
            }
        }

        [Command("warn")]
        public async Task WarnUser(IGuildUser user, [Remainder]string reason = "No reason provided")
        {
            await Context.Message.DeleteAsync();
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
            await Context.Message.DeleteAsync();
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
            await Context.Message.DeleteAsync();
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
            await Context.Message.DeleteAsync();
            await Context.Guild.AddBanAsync(userAccount);
            await Context.Channel.SendMessageAsync($"The user {userAccount} has been Banned, for {reason} from:{Context.Message.Author}");
            await Context.Guild.GetTextChannel(525136686431207425).SendMessageAsync($"The user {userAccount} was Banned for the following reason:\n{reason} from:{Context.Message.Author}");
        }

    }
}
