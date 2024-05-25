using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Arbitrage.commands.PrefixCommands
{
    public class TestCommand : BaseCommandModule
    {
        [Command("test")]
        public async Task testCommand(CommandContext tc)
        {
            await tc.Channel.SendMessageAsync($"Hello {tc.User.Username}");
        }
    };
}
