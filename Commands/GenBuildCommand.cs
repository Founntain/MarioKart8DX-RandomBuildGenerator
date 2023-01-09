using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Mk8RPBot.Classes;
using SkiaSharp;

namespace Mk8RPBot.Commands{
    public sealed class GenBuildCommand{
        public async Task ExecuteCommandAsync(SocketSlashCommand command, int genType, bool excludeInline, int amount = 1){
            var buildGenerator = new BuildGenerator();

            if(amount > 12){
                await command.RespondAsync("Can't generate more than 12 builds at the same time! Sorry for that :c");
                return;
            }

            if (genType == 0)
            {
                using var stream = await buildGenerator.Generate(amount, false, excludeInline);
                
                await command.RespondWithFileAsync(
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

                return;
            }
            
            if (genType == 1)
            {
                using var stream = await buildGenerator.Generate(amount, true, excludeInline);
                
                await command.RespondWithFileAsync(
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