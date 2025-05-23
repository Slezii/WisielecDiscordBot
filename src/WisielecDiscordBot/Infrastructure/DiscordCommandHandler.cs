﻿
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using WisielecDiscordBot.Infrastructure.Abstractions.Interfaces;
using WisielecDiscordBot.Infrastructure.Attributes;

namespace WisielecDiscordBot.Infrastructure
{
    [Injectable(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    internal class DiscordCommandHandler : IDiscordCommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        public DiscordCommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            _commands = commands;
            _client = client;
            _services = services;
        }

        public async Task InitializeAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            if (arg is not SocketUserMessage msg || msg.Author.IsBot) return;
            Console.WriteLine($"Message received: '{msg.Content}'");
            int argPos = 0;
            if (!msg.HasCharPrefix('!', ref argPos)) return;

            var context = new SocketCommandContext(_client, msg);
            await _commands.ExecuteAsync(context, argPos, _services);
        }
    }
}
