using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class PvPModule : ModuleBase<SocketCommandContext>
    {
        [Command("fight", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Fight(SocketGuildUser user)
        {
            UserAccount account1 = UserManager.GetAccount(Context.Message.Author);
            UserAccount account2 = UserManager.GetAccount(user);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author} want to fight with you!Do you want to accept the challenge?");
            var channel = await user.GetOrCreateDMChannelAsync();
            await Task.Delay(1000);
            var messages = await channel.GetMessagesAsync(1).FlattenAsync();
            foreach (var message in messages)
            {
                if (message.Content.Equals("yes"))
                {
                    await ReplyAsync("Battle was accept");
                    //StartFight();
                }
                else
                {
                    await ReplyAsync("Battle was declined");
                }
            }
        }
    }
}
