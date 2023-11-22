using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MkBuildBot.Commands;

public static class InfoCommand
{
    public static async Task PrintInfo(SocketSlashCommand command, int guildCount)
    {
        var embed = new EmbedBuilder
        {
            Title = "Mario Kart 8 Random Part Builder Bot",
            Timestamp = DateTime.Now,
            ThumbnailUrl = "https://mario.wiki.gallery/images/3/3d/MK8_Deluxe_Art_-_Crazy_Eight.png",
            Color = Color.Magenta,
            Url = "https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator"
        };

        embed.AddField("Need help?", "Need help using this bot? Or you think there is something wrong in general. Head over to [GitHub](https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator) and report it there.");

        embed.AddField("/info", "Shows this message", true);
        embed.AddField("/gen-build", "Generate (or multiple) builds", true);

        embed.AddField("Stats display", "Builds will include their complete stats like Top Speed, Handling, Traction etc. etc. This is only supported for solo builds and only for the switch version. If you generate multiple builds. Only the builds will show!");

        embed.AddField("Different Mii colors",
            $"You probably noticed different Mii Character colors. These indecate their Weight-Class.\n\n Gray are for **light**, blue for **medium** and red for **heavy** Miis. This is **only** important for the stat calculation of the build. " +
            $"\n\nIf you generate a build and get a Mii. Just use your current one c: It's just an information for you, to know which Mii-Weight-Class was used for this build! ");

        embed.AddField("Support!", "Type **/support** to get all ways to reach out to me if you have questions");

        embed.AddField("Thank you!", $"Thank you for using this bot. If you like my work I'm happy if you simply say thanks. {new Emoji("❤")} Or by giving the [GitHub-Repository a {new Emoji("⭐")}](https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator)");

        embed.Footer = new EmbedFooterBuilder
        {
            IconUrl = "https://osuplayer.founntain.dev/user/getProfilePicture?id=68c561ec-2313-43bc-8e1b-4227a2936e35",
            Text = $"Bot made by Founntain • Currently on {guildCount} servers active"
        };

        await command.RespondAsync(embed: embed.Build());
    }
}