using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using mk8bot.Classes;
using mk8bot.Commands;
using Newtonsoft.Json;

namespace mk8bot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private Config _config;

        static void Main(string[] args) =>
            new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            _client = new DiscordSocketClient();
            _client.Log += OnLog;
            _client.MessageReceived += OnMessageReceived;
            _client.Connected += OnConnected;
            _client.Ready += OnReady;

            _client.SlashCommandExecuted += SlashCommandHandler;

            var token = _config.Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        
        private async Task OnReady()
        {
            var commandRegisterer = new CommandRegisterer(_client);

            await commandRegisterer.RegisterCommands();
        }
        
        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            switch(command.Data.Name){
                case "info":
                    await new InfoCommand().PrintInfo(command, _client.Guilds.Count);
                    break;
                case "gen-build":
                    var gameVersion = (long) command.Data.Options.FirstOrDefault(x => x.Name == "game-version")?.Value;
                    var excludeInlineParam = (long) command.Data.Options.FirstOrDefault(x => x.Name == "exclude-inline-bikes")?.Value;
                    long amount = (long) (command.Data.Options.FirstOrDefault(x => x.Name == "amount")?.Value ?? (long) 1);

                    var excludeInline = excludeInlineParam != 0; //!= => true | == => false

                    new GenBuildCommand().ExecuteCommand(command, (int) gameVersion, excludeInline, (int) amount);

                    break;
                case "support":
                    await new SupportCommand().PrintSupport(command, _client.Guilds.Count);
                    
                    break;
                case "remind-owners":
                    await new RemindOwnersToUpdateCommand().PrintTest(command, _client);
                    break;
                default:
                    await command.RespondAsync($"There isn't an implementation for /{command.Data.Name} yet! :'c");
                    break;
            }
        }

        private Task OnLog(LogMessage message){
            if (message.Exception is CommandException cmdException)
            {
                Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()} failed to execute in {cmdException.Context.Channel}.");
                Console.WriteLine(cmdException);
            }
            else {
                Console.WriteLine($"[General/{message.Severity}] {message}");
            }

            return Task.CompletedTask;
        }

        private Task OnMessageReceived(SocketMessage msg){
            if(!msg.Content.StartsWith(_config.Prefix))
                return Task.CompletedTask;

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}info"))
            {
                // new InfoCommand().PrintInfo(msg, _config.Prefix, _client.Guilds.Count);
                return Task.CompletedTask;
            }

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}helpwiiu"))
            {
                new HelpWiiUCommand().PrintHelp(msg, _config.Prefix, _client.Guilds.Count);
                return Task.CompletedTask;
            }

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}help"))
            {
                new HelpCommand().PrintHelp(msg, _config.Prefix, _client.Guilds.Count);
                return Task.CompletedTask;
            }

            var args = msg.Content.Split(" ").ToList();
            args.RemoveAt(0);

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}genbuild"))
            {
                //new GenBuildCommand().ExecuteCommand(msg, args);
            }

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}genwiiu"))
            {
                new GenWiiUBuildCommand().ExecuteCommand(msg, args);
            }

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}gennames"))
            {
                new GenNamesBuildCommand().ExecuteCommand(msg, args);
            }

            if(msg.Content.ToLower().StartsWith($"{_config.Prefix}genwiiunames"))
            {
                new GenWiiUNamesBuildCommand().ExecuteCommand(msg, args);
            }

            return Task.CompletedTask;
        }

        private Task OnConnected(){
            _client.SetGameAsync($"/info | on {_client.Guilds.Count} servers", type: ActivityType.Watching);

            return Task.CompletedTask;
        }

        public static Embed GetBuildEmbed(int amount, bool wiiu = false, bool excludeInline = false){
            var embed = new EmbedBuilder{
                ImageUrl = $"attachment://build.png",
                Color = wiiu ? new Color(0x00AEFF) : new Color(0xFF0000),
                Title = amount > 1 ? "Your builds" : "Your build"
            };

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • {(wiiu ? "WiiU Version" : "Switch Version")}{(excludeInline ? " • Inline Bikes Excluded" : string.Empty)}"
            };

            return embed.Build();
        }
    }
}
