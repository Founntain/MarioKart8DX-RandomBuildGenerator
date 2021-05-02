using System.Collections.Generic;
using System.IO;
using System.Linq;
using Discord.WebSocket;
using mk8bot.Classes;

namespace mk8bot.Commands{
    public sealed class GenWiiUBuildCommand{
        public void ExecuteCommand(SocketMessage msg, IList<string> args){
            var buildGenerator = new BuildGenerator();

            switch(args.Count){
                case 0:
                    using(var stream = buildGenerator.Generate(true)){
                        msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", embed: Program.GetBuildEmbed(1, true));
                    }

                    return;
                case 1:
                    if(int.TryParse(args[0], out var x)){
                        if(x > 12){
                            msg.Channel.SendMessageAsync("Can't generate more than 12 builds at the same time!");
                            return;
                        }

                        using(var stream = buildGenerator.Generate(x, true).Result){
                            msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), $"{x}build.png", embed: Program.GetBuildEmbed(x, true));
                            return;
                        }
                    }

                    if(args[0].ToLower() != "[ni]"){
                        msg.Channel.SendMessageAsync("Invalid parameters!");
                        return;
                    }

                    using(var stream = buildGenerator.Generate(true, true)){
                        msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", embed: Program.GetBuildEmbed(1, true, true));
                    }

                    return;
                case 2:
                    var excludeInlineParam = args.FirstOrDefault(x => x.ToLower() == "[ni]");
                    var excludeInline = false;


                    if(excludeInlineParam != null){
                        excludeInline = true;
                        args.Remove(excludeInlineParam);
                    }

                    if(int.TryParse(args[0], out var y)){
                        using(var stream = buildGenerator.Generate(y, excludeInline: excludeInline).Result){
                            msg.Channel.SendFileAsync(new MemoryStream(stream.ToArray()), "build.png", embed: Program.GetBuildEmbed(1, true, excludeInline));
                        }

                        return;
                    }
                    break;
                default:
                    msg.Channel.SendMessageAsync("Invalid parameters!");
                    return;
            }
        }
    }
}