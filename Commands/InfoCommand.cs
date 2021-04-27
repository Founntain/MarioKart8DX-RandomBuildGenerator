using System;
using System.Globalization;
using Discord;
using Discord.WebSocket;

namespace mk8bot.Commands{
    public sealed class InfoCommand{
        public void PrintInfo(SocketMessage msg, string prefix, int guildCount){
            var embed = new EmbedBuilder{
                Title = "Mario Kart 8 Random Part Builder Bot",
                Timestamp = DateTime.Now,
                ThumbnailUrl = "https://mario.wiki.gallery/images/3/3d/MK8_Deluxe_Art_-_Crazy_Eight.png",
                Color = Color.Magenta,
                Url = "https://twitter.com/FounntainXD"
            };

            embed.AddField("Generate one build", $"{prefix}genbuild");
            embed.AddField("Generate multiple builds (max. 12)", $"{prefix}genbuild <number>");

            embed.AddField("Generate one build (WiiU only parts)", $"{prefix}genwiiubuild");
            embed.AddField("Generate multiple builds (max. 12) (WiiU only parts)", $"{prefix}genwiiubuild <number>");

            embed.AddField("Thank you!", $"Thank you for using this bot. If you like my work I'm happy if you simply say thanks. {new Emoji("❤")}");

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • Currently on {guildCount} servers active"
            };

            msg.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}