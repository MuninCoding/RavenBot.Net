using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class DeleteModule : ModuleBase<SocketCommandContext>
    {
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
    }
}
