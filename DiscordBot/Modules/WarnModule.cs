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
    //the class needs to be public
    public class WarnModule : ModuleBase<SocketCommandContext>
    {
        [Command("warn")]

        public async Task WarnUser(IGuildUser user, [Remainder]string reason = "No reason provided")
        {
            var userAccount = UserManager.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserManager.SaveAccounts();

            if (userAccount.NumberOfWarnings ==5)
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
            else if(userAccount.NumberOfWarnings >= 1)
            {
                var channel = await user.GetOrCreateDMChannelAsync();
                await channel.SendMessageAsync($"This is your {userAccount.NumberOfWarnings} Warning! for" + reason + " be careful!");
            }
        }
    }
}
