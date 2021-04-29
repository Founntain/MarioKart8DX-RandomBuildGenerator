using System.Collections.Generic;
using System.IO;
using Discord.WebSocket;
using mk8bot.Classes;

namespace mk8bot.Commands{
    public sealed class GenBuildCommand{
        public void ExecuteCommand(SocketMessage msg, IList<string> args){
            var buildGenerator = new BuildGenerator();

            if(args.Count == 0){
                using(var stream = buildGenerator.Generate()){
                    msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", embed: Program.GetBuildEmbed(1));
                }

                return;
            }

            if(args.Count == 1){
                if(int.TryParse(args[0], out var x)){
                    if(x > 12){
                        msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                        return;
                    }

                    using(var stream = buildGenerator.Generate(x).Result){
                        msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{x}build.png", embed: Program.GetBuildEmbed(x));
                        return;
                    }
                }
            }
        }
    }
}