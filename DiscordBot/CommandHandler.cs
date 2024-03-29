﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DiscordBot;
using DiscordBot.Core.LevelSystem;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                services: null);
            _client.MessageReceived += HandleCommandAsync;
            
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            if (context.User.IsBot) return;

            MessageLevelSystem.AddXpForMessage((SocketGuildUser)context.User, (SocketTextChannel)context.Channel);
            UserAccount account = UserManager.GetAccount(context.User);
            account.MessageCount++;
            UserManager.SaveAccounts();

            int argPos = 0;
            if (msg.HasCharPrefix(ConfigHandler.config.Prefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: null);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
