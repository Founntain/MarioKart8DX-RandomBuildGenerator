using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace mk8bot.Commands{
    public sealed class RemindOwnersToUpdateCommand{
        public async Task PrintTest(SocketSlashCommand command, DiscordSocketClient client){
            try
            {
                var ownersContacted = 0;
                
                foreach (var guild in client.Guilds)
                {
                    var ownerOfGuild = await client.GetUserAsync(guild.OwnerId);

                    try
                    {
                        await ownerOfGuild.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle("Important updates to the Mario Kart 8 RPBot")
                            .WithColor(Color.Red)
                            .WithDescription(
                                "Due to updates to the Discord policy for bots we have to use slash commands in the future." +
                                "\n\nTo keep using the bot you need to re-add the bot to your server. " +
                                "You should do this as soon as possible. The new rules apply at the **1st of May 2022**!")
                            .WithAuthor("A message from Founntain the developer of this Bot")
                            .WithFields(new EmbedFieldBuilder()
                                .WithName("How?")
                                .WithValue(
                                    "You can either do this by clicking on the **Add to Server** button on the bots profile." +
                                    "\nOr by kicking the bot and re-adding it with this **[link](https://discord.com/api/oauth2/authorize?client_id=836318982080167946&permissions=19456&scope=bot%20applications.commands)**"))
                            .WithFields(new EmbedFieldBuilder()
                                .WithName("Need Help?")
                                .WithValue(
                                    "If you have questions head over to **[GitHub](https://github.com/Founntain/MarioKart8DX-RandomBuildGenerator)** or write me an E-Mail: **7@founntain.dev**"))
                            .Build());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                    ownersContacted++;
                }

                await command.RespondAsync($"Successfully reminded {ownersContacted} owners");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}