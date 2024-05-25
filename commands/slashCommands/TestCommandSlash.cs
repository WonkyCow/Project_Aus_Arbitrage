using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Arbitrage.commands.slashCommands
{
    internal class TestCommandSlash : ApplicationCommandModule
    {
        [SlashCommand("test", "This is a test slash command")]
        public async Task TestSlashCommand(InteractionContext cntxt)
        {
            try
            {
                //await cntxt.Channel.SendMessageAsync($"Hello World");
                //await cntxt.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Hello World"));
                await cntxt.DeferAsync();

                await cntxt.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Hello World"));
                Console.WriteLine("Test message sent!");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
