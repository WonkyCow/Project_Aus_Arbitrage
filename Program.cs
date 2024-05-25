using Arbitrage_Test_Bot;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using TestApp;
using DSharpPlus.SlashCommands;
using Project_Arbitrage.commands.PrefixCommands;
using Project_Arbitrage.commands.slashCommands;
using Project_Arbitrage;

namespace TestApp
{
    internal class Program
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }

        static async Task Main(string[] args)
        {
            //declare new json reader
            var jsonReader = new JSONReader();
            //call json reader
            await jsonReader.ReadJSON();

            //set the Discord App Configuration
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All, 
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            Client.Ready += Client_Ready;


            //set the command configuration
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDefaultHelp = true
            };

            //initialise the CommandsNextExtension property
            Commands = Client.UseCommandsNext(commandsConfig);

            //Enable slash commands
            var slashCommandsConfiguration = Client.UseSlashCommands();

            //register command class
            Commands.RegisterCommands<TestCommand>();

            //register slash commands
            slashCommandsConfiguration.RegisterCommands<TestCommandSlash>();
            slashCommandsConfiguration.RegisterCommands<ArbitrageChecker>();


            //Connect to the discord gateway
            await Client.ConnectAsync();

            //Ensure the bot runs indefinitely, while the program is running
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}   
