using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord.WebSocket;
using Mk8RPBot.Classes;

namespace Mk8RPBot.Commands{
    public sealed class GenNamesBuildCommand{
        public async void ExecuteCommandAsync(SocketMessage msg, IList<string> args){
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
                    await msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                    return;
                }

                using var stream = await buildGenerator.GenerateWithNames(args, excludeInline: excludeInline);
                
                await msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{amount}build.png", embed: Program.GetBuildEmbed(amount, excludeInline: excludeInline));
                return;
            }

            await msg.Channel.SendMessageAsync("Please enter at least 2 names");
        }
    }
}