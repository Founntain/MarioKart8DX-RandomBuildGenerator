using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MkBuildBot.Classes;

namespace MkBuildBot.Commands;

public static class GenBuildCommand
{
    public static async Task ExecuteCommandAsync(SocketSlashCommand command, int genType, bool excludeInline,
        int amount = 1)
    {
        var buildGenerator = new BuildGenerator();

        if (amount > 12)
        {
            await command.RespondAsync("Can't generate more than 12 builds at the same time! Sorry for that :c");
            return;
        }

        switch (genType)
        {
            case 0:
            {
                using var stream = await buildGenerator.Generate(amount, false, excludeInline);

                await command.RespondWithFileAsync(
                    stream,
                    "build.png",
                    string.Empty,
                    null,
                    false,
                    false,
                    null,
                    null,
                    Program.GetBuildEmbed(amount, excludeInline),
                    RequestOptions.Default);

                return;
            }
            case 1:
            {
                using var stream = await buildGenerator.Generate(amount, true, excludeInline);

                await command.RespondWithFileAsync(
                    stream,
                    "build.png",
                    string.Empty,
                    null,
                    false,
                    false,
                    null,
                    null,
                    Program.GetBuildEmbed(amount, true, excludeInline),
                    RequestOptions.Default);

                return;
            }
        }
    }
}