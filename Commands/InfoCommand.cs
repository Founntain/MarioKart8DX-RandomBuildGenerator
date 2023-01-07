using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Mk8RPBot.Commands{
    public sealed class InfoCommand{
        public async Task PrintInfo(SocketSlashCommand command, int guildCount){
            var embed = new EmbedBuilder{
                Title = "Mario Kart 8 Random Part Builder Bot",
                Timestamp = DateTime.Now,
                ThumbnailUrl = "https://mario.wiki.gallery/images/3/3d/MK8_Deluxe_Art_-_Crazy_Eight.png",
                Color = Color.Magenta,
                Url = "https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator"
            };

            embed.AddField("Need help?", $"Need help using this bot? Or you think there is something wrong in general. Head over to [GitHub](https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator) and report it there.", false);

            embed.AddField("/info", $"Shows this message", true);
            embed.AddField("/gen-build", $"Generate (or multiple builds) with", true);
            
            embed.AddField("Support!", $"Type **/support** to get all ways to reach out to me if you have questions");
            
            embed.AddField("Thank you!", $"Thank you for using this bot. If you like my work I'm happy if you simply say thanks. {new Emoji("❤")}");

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • Currently on {guildCount} servers active"
            };

            await command.RespondAsync(embed: embed.Build());
        }
    }
}