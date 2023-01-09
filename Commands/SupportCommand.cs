using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Mk8RPBot.Commands{
    public sealed class SupportCommand{
        public async Task PrintSupport(SocketSlashCommand command, int guildCount){
            var embed = new EmbedBuilder{
                Title = "Mario Kart 8 Random Part Builder Bot",
                Timestamp = DateTime.Now,
                ThumbnailUrl = "https://osuplayer.founntain.dev/api/users/getProfilePicture?username=Founntain",
                Color = Color.Magenta,
                Url = "https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator"
            };

            embed.AddField("Github", $"[Founntain/MarioKart8DX-RandomBuildGenerator](https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator)");
            embed.AddField("Reddit", $"[u/Founntain](https://www.reddit.com/user/Founntain/)",  true);
            embed.AddField("Twitter", $"[FounntainXD](https://twitter.com/FounntainXD)", true);
            embed.AddField("E-Mail", $"7@founntain.dev", true);

            embed.AddField("Thank you!", $"Thank you for using this bot. If you like my work I'm happy if you simply say thanks. {new Emoji("❤")} Or by giving the [GitHub-Repository a {new Emoji("⭐")}](https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator)");

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://osuplayer.founntain.dev/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • Currently on {guildCount} servers active"
            };

            await command.RespondAsync(embed: embed.Build());
        }
    }
}