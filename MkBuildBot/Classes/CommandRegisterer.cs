using System;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace MkBuildBot.Classes;

public class CommandRegisterer
{
    private readonly DiscordSocketClient _client;

    public CommandRegisterer(DiscordSocketClient client)
    {
        _client = client;
    }

    private async Task RegisterInfoCommand()
    {
        var command = new SlashCommandBuilder();
        command.WithName("info");
        command.WithDescription("This command gives you a handy description/info about the bot!");

        await RegisterCommand(command);
    }

    private async Task RegisterSupportCommand()
    {
        var command = new SlashCommandBuilder();
        command.WithName("support");
        command.WithDescription("This command shows you all ways how you can contact me for help.");

        await RegisterCommand(command);
    }

    //Default Generate Commands
    private async Task RegisterGenBuildCommand()
    {
        var command = new SlashCommandBuilder()
            .WithName("gen-build")
            .WithDescription("Generates a build for the selected game version.")
            .AddOption(new SlashCommandOptionBuilder {IsRequired = true}
                .WithName("game-version")
                .WithDescription("Switch or WiiU Version?")
                .AddChoice("Switch", 0)
                .AddChoice("WiiU", 1)
                .WithType(ApplicationCommandOptionType.Integer))
            .AddOption(new SlashCommandOptionBuilder {IsRequired = true}
                .WithName("exclude-inline-bikes")
                .WithDescription("Do you want to exclude inline drifting bikes?")
                .AddChoice("No", 0)
                .AddChoice("Yes", 1)
                .WithType(ApplicationCommandOptionType.Integer))
            .AddOption(new SlashCommandOptionBuilder {IsRequired = true}
                .WithName("exclude-dlc-characters")
                .WithDescription("Do you want to exclude characters introduced with the Booster Course Pass?")
                .AddChoice("No", 0)
                .AddChoice("Yes", 1)
                .WithType(ApplicationCommandOptionType.Integer))
            .AddOption(new SlashCommandOptionBuilder {IsRequired = false}
                .WithName("amount")
                .WithDescription("How many builds do you want to generate?")
                .WithType(ApplicationCommandOptionType.Integer)
            );

        await RegisterCommand(command);
    }

    public async Task RegisterCommands()
    {
        await RegisterInfoCommand();
        await RegisterSupportCommand();
        await RegisterGenBuildCommand();
    }

    private async Task RegisterCommand(SlashCommandBuilder command)
    {
        await RegisterCommandGlobally(command);
    }

    private async Task RegisterCommandGlobally(SlashCommandBuilder command)
    {
        try
        {
            await _client.CreateGlobalApplicationCommandAsync(command.Build());
        }
        catch (HttpException ex)
        {
            var json = JsonConvert.SerializeObject(ex.Errors, Formatting.Indented);

            Console.WriteLine(json);
        }
    }
}