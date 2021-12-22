using System;
using Discord;
using Discord.WebSocket;

namespace mk8bot.Commands{
    public sealed class HelpWiiUCommand{
        public void PrintHelp(SocketMessage msg, string prefix, int guildCount){
            var embed = new EmbedBuilder{
                Title = "WiiU Commands",
                Timestamp = DateTime.Now,
                ThumbnailUrl = "https://mario.wiki.gallery/images/3/3d/MK8_Deluxe_Art_-_Crazy_Eight.png",
                Color = Color.Magenta
            };

            embed.AddField("Generate a build", $"{prefix}genWiiU", true);
            embed.AddField("Generate multiple builds (max. 12)", $"{prefix}genWiiU <number>", true);

            embed.AddField("Include names for multiple builds", $"Instead of printing Build 1, Build 2 etc. you can include names instead");

            embed.AddField("Generate multiple builds with names (max. 12)", $"{prefix}genWiiUNames <names seperated by spaces>", true);

            embed.AddField("Names example", $"{prefix}genWiiUNames name1 name2 name3", true);

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • All commands are case insensitive"
            };

            msg.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}