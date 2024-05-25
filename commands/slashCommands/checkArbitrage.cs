using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Project_Arbitrage.commands.slashCommands
{
    internal class ArbitrageChecker : ApplicationCommandModule
    {
        [SlashCommand("CheckArbitrage", "Checks for arbitrage opportunities")]
        public async Task CheckArbitrageSlashCommand(InteractionContext context)
        {
            try
            {
                // Defer the interaction to acknowledge receipt of the command
                await context.DeferAsync();
                Console.WriteLine("DerferAsync successful");

                // Perform the arbitrage check
                string result = await CheckForArbitrage();
                Console.WriteLine("CheckForArbitrage successful");

                // Send the result to the channel where the command was invoked
                await SendMessageToChannel(context, result);
                Console.Write($"result received: {result}\n");
                Console.WriteLine("SendMessageToChannel successful");

                //await context.Channel.SendMessageToChannel(context, result);
                //await context.EditResponseAsync(new DiscordWebhookBuilder().WithContent(result));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex.ToString());
                await context.EditResponseAsync(new DiscordWebhookBuilder().WithContent("An error occurred while checking for arbitrage."));
            }
        }

        private async Task<string> CheckForArbitrage()
        {
            Console.WriteLine("CheckForArbitrage Initiated");
            string result = "";

            var dataList = new List<BettingSiteData>();

            var sportsbetData = await new DebugScrapers.SportsbetDebug().ScrapeData();
            Console.WriteLine("Sportsbet Scrape successful");
            var ladbrokesData = await new DebugScrapers.LadbrokesDebug().ScrapeData();
            Console.WriteLine("Ladbrokes Scrape successful");

            dataList.Add(sportsbetData);
            dataList.Add(ladbrokesData);
            Console.WriteLine("Data added to datalist successfully");

            var calculator = new ArbitrageCalculator();
            result = calculator.CalculateArbitrage(dataList);
            Console.WriteLine("Arbitrage result identified successfully");

            return result;
        }

        private async Task SendMessageToChannel(InteractionContext context, string message)
        {
            // Send the message to the channel where the command was invoked
            //message = "test";
            try
            {
                Console.WriteLine("Trying to send message");
                Console.WriteLine($"Message: {message}\n");
                await context.EditResponseAsync(new DiscordWebhookBuilder().WithContent(message));
                Console.WriteLine("Message sent!");
            }
            catch
            {
                Console.WriteLine("Error sending message to discord server");
            }
            
        }
    }
}