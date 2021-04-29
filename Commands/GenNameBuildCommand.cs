using System.Collections.Generic;
using System.IO;
using Discord.WebSocket;
using mk8bot.Classes;

namespace mk8bot.Commands{
    public sealed class GenWiiUNamesBuildCommand{
        public void ExecuteCommand(SocketMessage msg, IList<string> args){
            var buildGenerator = new BuildGenerator();
            var amount = args.Count;

            if(amount > 1){
                if(amount > 12){
                    msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                    return;
                }

                using(var stream = buildGenerator.GenerateWithNames(args, true).Result){
                    msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{amount}build.png", embed: Program.GetBuildEmbed(amount, true));
                    return;
                }
            }

            msg.Channel.SendMessageAsync("Please enter at least 2 names");
        }
    }
}