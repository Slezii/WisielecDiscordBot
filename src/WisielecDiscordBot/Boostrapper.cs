using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WisielecDiscordBot.Infrastructure;
using WisielecDiscordBot.Infrastructure.Extensions;

namespace WisielecDiscordBot
{
    internal class Boostrapper
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private IServiceProvider _services;

        public Boostrapper()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                GatewayIntents = GatewayIntents.Guilds |
                     GatewayIntents.GuildMessages |
                     GatewayIntents.MessageContent
            });

            _commands = new CommandService();

            _client.Log += LogAsync;
        }

        public async Task RunAsync()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("Configs/appsettings.json", optional: false)
                .Build();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands).AddSingleton(_services)
                .AddAttributedInjectables()
                .BuildServiceProvider();

            await _services.GetRequiredService<DiscordCommandHandler>().InitializeAsync();

            string token = config["token"];
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}
