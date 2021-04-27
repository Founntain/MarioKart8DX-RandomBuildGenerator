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

            embed.AddField("Generate one build", $"{prefix}genbuild", true);
            embed.AddField("Generate multiple builds (max. 12)", $"{prefix}genbuild <number>", true);
            embed.AddField("Deluxe only?", $"The bot was made mainly for the switch version. However if you get a build with switch exclusive parts etc. just generate a new one!");
            
            embed.AddField("Thank you!", $"Thank you for using the bot. If you like my work I'm happy if you simply say thank you etc. {new Emoji("❤")}");

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • Currently on {guildCount} servers active"
            };

            msg.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}