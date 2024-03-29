﻿using Discord;
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
    public class XPModule : ModuleBase<SocketCommandContext>
    {
        [Command("stats")]
        public async Task XP(SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            UserAccount account;
            if(user != null)
            {
                account = UserManager.GetAccount(user);
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
            }
            uint xp = account.XP;
            uint level = (uint)Math.Sqrt(xp / 50);

            await Context.Channel.SendMessageAsync($"{account.Name} has {account.XP} XP, and is level {level},\n{account.Name} has {account.NumberOfWarnings} Warnings and has written {account.MessageCount} Messages!");
        }

        [Command("addXP")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddXP(uint xp, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            UserAccount account;
            if (user != null)
            {
                account = UserManager.GetAccount(user);
            }
            else
            {
                account = UserManager.GetAccount(Context.Message.Author);
            }
            account.XP += xp;
            UserManager.SaveAccounts();
            await Context.Channel.SendMessageAsync($"{user} gained {xp} XP.");
        }
    }
}