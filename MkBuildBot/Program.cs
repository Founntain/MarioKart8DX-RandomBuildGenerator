using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MkBuildBot.Classes;
using MkBuildBot.Commands;
using Newtonsoft.Json;

namespace MkBuildBot;

public class Program
{
    private DiscordSocketClient _client = null!;
    private Config? _config;

    public static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
        if (!File.Exists("config.json"))
        {
            Console.WriteLine("Couldn't find the config file...");
            Console.WriteLine("BOT STOPPING");

            return;
        }

        _config = JsonConvert.DeserializeObject<Config>(await File.ReadAllTextAsync("config.json"));

        if (_config == null || string.IsNullOrWhiteSpace(_config?.Token))
        {
            Console.WriteLine("Couldn't find the config or there was no token specified in the config...");
            Console.WriteLine("BOT STOPPING");

            return;
        }

        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.None,
            UseInteractionSnowflakeDate = false
        });

        _client.Log += OnLog;
        _client.Connected += OnConnected;
        _client.Ready += OnReady;

        _client.SlashCommandExecuted += SlashCommandHandler;

        var token = _config.Token;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private async Task<Task> OnReady()
    {
        var commandRegisterer = new CommandRegisterer(_client);

        await commandRegisterer.RegisterCommands();

        return Task.CompletedTask;
    }

    private async Task<Task> SlashCommandHandler(SocketSlashCommand command)
    {
        switch (command.Data.Name)
        {
            case "info":
                await InfoCommand.PrintInfo(command, _client.Guilds.Count);

                break;
            case "gen-build":
                var gameVersion = (long) command.Data.Options.First(x => x.Name == "game-version").Value;
                var excludeInlineParam = (long) command.Data.Options.First(x => x.Name == "exclude-inline-bikes").Value;
                var isDLCCharactersExcluded = (long)command.Data.Options.FirstOrDefault(x => x.Name == "amount").Value == 1;
                var amount = (long) (command.Data.Options.FirstOrDefault(x => x.Name == "amount")?.Value ?? (long) 1);
                var excludeInline = excludeInlineParam != 0; //!= => true | == => false

                await GenBuildCommand.ExecuteCommandAsync(command, (int) gameVersion, excludeInline, (int) amount);

                break;
            case "support":
                await SupportCommand.PrintSupport(command, _client.Guilds.Count);

                break;
            default:
                await command.RespondAsync($"There isn't an implementation for /{command.Data.Name} yet! :'c");

                break;
        }

        return Task.CompletedTask;
    }

    private Task OnLog(LogMessage message)
    {
        if (message.Exception is CommandException cmdException)
        {
            Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()} failed to execute in {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException);
        }
        else
        {
            Console.WriteLine($"[General/{message.Severity}] {message}");
        }

        return Task.CompletedTask;
    }

    private async Task<Task> OnConnected()
    {
        await _client.SetGameAsync($"/info | on {_client.Guilds.Count} servers", type: ActivityType.Watching);

        return Task.CompletedTask;
    }

    public static Embed GetBuildEmbed(int amount, bool wiiu = false, bool excludeInline = false)
    {
        var embed = new EmbedBuilder
        {
            ImageUrl = "attachment://build.png",
            Color = wiiu ? new Color(0x00AEFF) : new Color(0xFF0000),
            Title = amount > 1 ? "Your builds" : "Your build",
            Footer = new EmbedFooterBuilder
            {
                IconUrl = "https://osuplayer.founntain.dev/user/getProfilePicture?id=68c561ec-2313-43bc-8e1b-4227a2936e35",
                Text = $"Bot made by Founntain • {(wiiu ? "WiiU Version" : "Switch Version")}{(excludeInline ? " • Inline Bikes Excluded" : string.Empty)}"
            }
        };

        return embed.Build();
    }
}