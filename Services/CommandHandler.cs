using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using jane.Models;
using jane.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace jane.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfigurationRoot config;

        public CommandHandler(
            DiscordSocketClient _client,
            CommandService _commands,
            IServiceProvider _provider,
            IConfigurationRoot _config
        )
        {
            client = _client;
            commands = _commands;
            serviceProvider = _provider;
            config = _config;

            client.MessageReceived += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            var message = s as SocketUserMessage;
            if (message == null) return;
            if (message.Author.Id == client.CurrentUser.Id) return;

            var context = new SocketCommandContext(client, message);

            int argPos = 0;
            if (message.Author.Id == config.GetRequiredSection("Settings").Get<Settings>().MarvinID)
            {
                return;
            }
            else if (message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                var result = await commands.ExecuteAsync(context, argPos, serviceProvider);

                if (!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync("I'm so sorry, I couldn't find that command. Want to try again?");
                }
            }
            else
            {
                await CommonResponse(context);
            }
        }

        private async Task MarvinResponse(SocketCommandContext context)
        {
            List<string> responses = serviceProvider.GetRequiredService<IConfigurationRoot>().GetRequiredSection("Responses").Get<List<string>>();

            Random rand = serviceProvider.GetRequiredService<Random>();

            int max = responses.Count - 1;
            string response = responses[rand.Next(0, max)];

            int bingo = rand.Next(1, 100);

            if (bingo <= 15)
            {
                await context.Channel.SendMessageAsync(response);
            }
        }

        private async Task CommonResponse(SocketCommandContext context)
        {
            List<string> responses = serviceProvider.GetRequiredService<IConfigurationRoot>().GetRequiredSection("Responses").Get<List<string>>();

            Random rand = serviceProvider.GetRequiredService<Random>();

            int max = responses.Count - 1;

            string response = responses[rand.Next(0, max)];

            int bingo = rand.Next(1, 100);

            if (bingo <= 3)
            {
                await context.Message.ReplyAsync(response);
            }
        }
    }
}