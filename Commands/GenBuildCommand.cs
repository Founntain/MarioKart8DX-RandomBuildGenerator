using System.IO;
using Discord;
using Discord.WebSocket;
using mk8bot.Classes;

namespace mk8bot.Commands{
    public sealed class GenBuildCommand{
        public void ExecuteCommand(SocketSlashCommand command, int genType, bool excludeInline, int amount = 1){
            var buildGenerator = new BuildGenerator();

            if(amount > 12){
                command.RespondAsync("Can't generate more than 12 builds at the same time! Sorry for that :c");
                return;
            }

            switch(genType){
                case 0:
                    using(var stream = buildGenerator.Generate(amount, false, excludeInline).Result){
                        command.RespondWithFileAsync(
                            new MemoryStream(stream.ToArray()),
                            "build.png", 
                            string.Empty, 
                            null, 
                            false, 
                            false, 
                            null, 
                            null,
                            Program.GetBuildEmbed(amount, false, excludeInline: excludeInline), 
                            RequestOptions.Default);
                    }

                    return;
                case 1:
                    using(var stream = buildGenerator.Generate(amount, true, excludeInline).Result){
                        command.RespondWithFileAsync(
                            new MemoryStream(stream.ToArray()),
                            $"build.png", 
                            string.Empty, 
                            null, 
                            false, 
                            false, 
                            null, 
                            null,
                            Program.GetBuildEmbed(amount, true, excludeInline: excludeInline), 
                            RequestOptions.Default);

                        return;
                    }       
            }
        }
    }
}