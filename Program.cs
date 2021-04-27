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
    class Program
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

            if(msg.Content.ToLower() == $"{Config.Prefix}getbuild")
            {
                using(var stream = new BuildGenerator().Generate()){
                    msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", "Your build");
                }
            }

            return Task.CompletedTask;
        }
    }
}
