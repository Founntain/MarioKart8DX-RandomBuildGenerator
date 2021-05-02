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

            embed.AddField("Need help?", $"You find a description of all commands by using the help commands listed below.\n**ALL commands are case *insensitive* **", false);

            embed.AddField("Show all default generation commands", $"{prefix}help", true);
            embed.AddField("Show all WiiU generation commands", $"{prefix}helpWiiU", true);

            embed.AddField("Optional Parameters", $"If you include a `[ni]` inside of your parameters. The bot will generate a (or multiple) build(s) **excluding** inline bikes", false);

            embed.AddField("Thank you!", $"Thank you for using this bot. If you like my work I'm happy if you simply say thanks. {new Emoji("❤")}");

            embed.Footer = new EmbedFooterBuilder{
                IconUrl = "https://api.founntain.de/api/users/getProfilePicture?username=Founntain",
                Text = $"Bot made by Founntain • Currently on {guildCount} servers active"
            };

            msg.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}