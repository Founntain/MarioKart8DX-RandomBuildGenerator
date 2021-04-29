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
        private DiscordSocketClient Client;
        private Config Config;

        static void Main(string[] args) =>
            new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));

            Client = new DiscordSocketClient();
            Client.Log += OnLog;
            Client.MessageReceived += OnMessageReceived;
            Client.Connected += OnConnected;

            var token = this.Config.Token;

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            await Task.Delay(-1);
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
            if(!msg.Content.StartsWith(Config.Prefix))
                return Task.CompletedTask;

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}info"))
            {
                new InfoCommand().PrintInfo(msg, Config.Prefix, Client.Guilds.Count);
                return Task.CompletedTask;
            }

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}helpwiiu"))
            {
                new HelpWiiUCommand().PrintHelp(msg, Config.Prefix, Client.Guilds.Count);
                return Task.CompletedTask;
            }

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}help"))
            {
                new HelpCommand().PrintHelp(msg, Config.Prefix, Client.Guilds.Count);
                return Task.CompletedTask;
            }

            var args = msg.Content.Split(" ").ToList();
            args.RemoveAt(0);

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}genbuild"))
            {
                new GenBuildCommand().ExecuteCommand(msg, args);
            }

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}genwiiu"))
            {
                new GenWiiUBuildCommand().ExecuteCommand(msg, args);
            }

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}gennames"))
            {
                new GenNamesBuildCommand().ExecuteCommand(msg, args);
            }

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}genwiiunames"))
            {
                new GenWiiUNamesBuildCommand().ExecuteCommand(msg, args);
            }

            return Task.CompletedTask;
        }

        private Task OnConnected(){
            Client.SetGameAsync($"{Config.Prefix}info for help", type: ActivityType.Watching);

            return Task.CompletedTask;
        }

        public static Embed GetBuildEmbed(int amount, bool wiiu = false){
            var embed = new EmbedBuilder{
                ImageUrl = amount > 1 ? $"attachment://{amount}build.png" : "attachment://build.png",
                Color = wiiu ? new Color(0x00AEFF) : new Color(0xFF0000)
            };

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • {(wiiu ? "WiiU Version" : "Switch Version")}"
            };

            return embed.Build();
        }
    }
}
