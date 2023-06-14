using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using jane.Models;
using jane.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace jane.Modules
{
    public class ChoreModule : ModuleBase<SocketCommandContext>
    {
        private readonly IServiceProvider ServiceProvider;

        public ChoreModule(IServiceProvider services)
        {
            ServiceProvider = services;
        }

        [Command("Chores")]
        [Summary("Get's a list of chores for the current day.")]
        public async Task GetChoresAsync([Remainder] string forDate = null)
        {
            string response;
            DateTime date;
            DbService dbService = ServiceProvider.GetRequiredService<DbService>();

            if (forDate is null)
            {
                date = DateTime.Today;
            }
            else
            {
                try
                {
                    date = DateTime.ParseExact(forDate, "d", new CultureInfo("en-US"));
                }
                catch
                {
                    await ReplyAsync("That date format is incorrect, please try again.");
                    return;
                }
            }

            ReplyAsync("Checking for matching results, and if no results are found, creating the chore list for " + date.ToString() + ". This may take a moment.");

            dbService.FindOrCreateChoreList(date);

            ReplyAsync("Chore list in place, checking completions of chores.");

            response = dbService.GetIncompleteChores();

            await ReplyAsync(response);
        }
    }
}