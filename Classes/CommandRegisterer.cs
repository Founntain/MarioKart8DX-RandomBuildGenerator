using System;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace mk8bot.Classes{
    public class CommandRegisterer{
        private readonly DiscordSocketClient _client;
        private const ulong GUILD_ID = 263840330586128384;

        public CommandRegisterer(DiscordSocketClient client)
        {
            _client = client;
        }

        private async Task RegisterInfoCommand(){
            var command = new SlashCommandBuilder();
            command.WithName("info");
            command.WithDescription("This command gives you a handy and description info about the bot!");

            await RegisterCommandGlobally(command);
        }
        
        private async Task RegisterSupportCommand(){
            var command = new SlashCommandBuilder();
            command.WithName("support");
            command.WithDescription("This command shows you all ways how you can contact me for help.");

            await RegisterCommandGlobally(command);
        }

        //Default Generate Commands
        private async Task RegisterGenBuildCommand(){
            var command = new SlashCommandBuilder()
                .WithName("gen-build")
                .WithDescription("Generates a build for the selected game version.")
                .AddOption(new SlashCommandOptionBuilder{ IsRequired = true }
                    .WithName("game-version")
                    .WithDescription("Switch or WiiU Version?")    
                    .AddChoice("Switch", 0)
                    .AddChoice("WiiU", 1)
                    .WithType(ApplicationCommandOptionType.Integer))
                .AddOption(new SlashCommandOptionBuilder{ IsRequired = true }
                    .WithName("exclude-inline-bikes")
                    .WithDescription("Do you want to exclude inline drifting bikes?")    
                    .AddChoice("No", 0)
                    .AddChoice("Yes", 1)
                    .WithType(ApplicationCommandOptionType.Integer))
                .AddOption(new SlashCommandOptionBuilder{ IsRequired = false }
                    .WithName("amount")
                    .WithDescription("How many builds do you want to generate?")
                    .WithType(ApplicationCommandOptionType.Integer)
            );

            await RegisterCommandGlobally(command);
        }

        private async Task RegisterRemindOwnersCommand(){
            var command = new SlashCommandBuilder();
            command.WithName("remind-owners");
            command.WithDescription("Remind owners to re-add the bot");

            await RegisterCommandOnGuild(command);
        }

        public async Task RegisterCommands(){
            await RegisterInfoCommand();
            await RegisterSupportCommand();
            await RegisterGenBuildCommand();
            await RegisterRemindOwnersCommand();
        }

        private async Task RegisterCommandGlobally(SlashCommandBuilder command){
            try
            {
                var guild = _client.GetGuild(GUILD_ID);

                await _client.CreateGlobalApplicationCommandAsync(command.Build());
                await guild.CreateApplicationCommandAsync(command.Build());
            }
            catch (HttpException ex)
            {
                var json = JsonConvert.SerializeObject(ex.Errors, Formatting.Indented);

                Console.WriteLine(json);
            }
        }
        
        private async Task RegisterCommandOnGuild(SlashCommandBuilder command){
            try
            {
                var guild = _client.GetGuild(GUILD_ID);

                await guild.CreateApplicationCommandAsync(command.Build());
            }
            catch (HttpException ex)
            {
                var json = JsonConvert.SerializeObject(ex.Errors, Formatting.Indented);

                Console.WriteLine(json);
            }
        }
    }
}