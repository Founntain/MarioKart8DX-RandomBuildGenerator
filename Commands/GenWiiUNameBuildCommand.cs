using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord.WebSocket;
using mk8bot.Classes;

namespace mk8bot.Commands{
    public sealed class GenWiiUNamesBuildCommand{
        public void ExecuteCommand(SocketMessage msg, IList<string> args){
            var buildGenerator = new BuildGenerator();
            
            var excludeInline = false;
            var excludeInlineParam = args.FirstOrDefault(x => x.ToLower() == "[ni]");

            if(excludeInlineParam != null){
                excludeInline = true;
                args.Remove(excludeInlineParam);
            }

            var amount = args.Count;

            if(amount > 1){
                if(amount > 12){
                    msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                    return;
                }

                using(var stream = buildGenerator.GenerateWithNames(args, true, excludeInline).Result){
                    msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{amount}build.png", embed: Program.GetBuildEmbed(amount, true, excludeInline));
                    return;
                }
            }

            msg.Channel.SendMessageAsync("Please enter at least 2 names");
        }
    }
}