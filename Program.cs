using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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

            var args = msg.Content.Split(" ").ToList();
            args.RemoveAt(0);

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}genbuild"))
            {
                var buildGenerator = new BuildGenerator();

                if(args.Count == 0){
                    using(var stream = buildGenerator.Generate()){
                        msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", "Your build");
                    }

                    return Task.CompletedTask;
                }

                if(args.Count == 1){
                    if(int.TryParse(args[0], out var x)){
                        if(x > 12){
                            msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                            return Task.CompletedTask;
                        }

                        using(var stream = buildGenerator.Generate(x).Result){
                            msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{x}build.png", $"Your {x} builds");
                            return Task.CompletedTask;
                        }
                    }
                }
            }

            if(msg.Content.ToLower().StartsWith($"{Config.Prefix}genwiiubuild"))
            {
                var buildGenerator = new BuildGenerator();

                if(args.Count == 0){
                    using(var stream = buildGenerator.Generate(true)){
                        msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", "Your build");
                    }

                    return Task.CompletedTask;
                }

                if(args.Count == 1){
                    if(int.TryParse(args[0], out var x)){
                        if(x > 12){
                            msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                            return Task.CompletedTask;
                        }

                        using(var stream = buildGenerator.Generate(x, true).Result){
                            msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{x}build.png", $"Your {x} builds");
                            return Task.CompletedTask;
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        private Task OnConnected(){
            Client.SetGameAsync("$$info for help", type: ActivityType.Watching);

            return Task.CompletedTask;
        }
    }
}
