using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using jane.Models;
using jane.Services;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace jane
{
	public class Startup
	{
		private IConfigurationRoot Config;

		private string runtimeEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		public Startup()
		{
			if (runtimeEnvironment is null)
			{
				runtimeEnvironment = "Development";
			}

			IConfigurationBuilder builder = new ConfigurationBuilder()
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.AddJsonFile($"appsettings.{runtimeEnvironment}.json")
			.AddJsonFile("Responses.json");

			Config = builder.Build();
		}

		public static async Task StartAsync(string[] args = null)
		{
			Startup startup = new Startup();
			await startup.RunAsync();
		}

		private async Task RunAsync()
		{
			ServiceCollection services = new ServiceCollection();
			ConfigureServices(services);

			ServiceProvider provider = services.BuildServiceProvider();
			provider.GetRequiredService<LoggerService>();
			provider.GetRequiredService<CommandHandler>();

			await provider.GetRequiredService<StartupService>().StartConnectionAsync();
			await Task.Delay(-1);
		}

		private void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(Config)
			.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Verbose,
				MessageCacheSize = 1000,
				GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
			}))
			.AddSingleton(new CommandService(new CommandServiceConfig
			{
				LogLevel = LogSeverity.Verbose,
				DefaultRunMode = RunMode.Async
			}))
			.AddSingleton<CommandHandler>()
			.AddSingleton<StartupService>()
			.AddSingleton<LoggerService>()
			.AddSingleton<DbService>()
			.AddSingleton<Random>();
		}
	}
}