using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;

namespace Project_Arbitrage
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();


        /**
         * Main Call function
         * Defines the information to be sent to the discord webhook URL
         */
        static async Task Main(string[] args)
        {
            string webhookUrl = "https://discord.com/api/webhooks/1236561732395077705/bNT7gn5pwS4KHBYwxaMResYgueYxUM02EL_8EI1KEold1K9r3rUgUax7uXfqj2PI9xn2";
            string sportsbet = new Sportsbet().ScrapeData().Result; // Assuming ScrapeData is an async method

            if (!string.IsNullOrEmpty(sportsbet))
            {
                await SendDiscordNotification(webhookUrl, sportsbet);
            }
        }
        /**
         * Send Notification function
         * Sends a message to discord via a Discord webhook, 
         */
        static async Task SendDiscordNotification(string webhookUrl, string message)
        {
            var content = new StringContent("{\"content\": \"" + message + "\", \"username\": \"Sportsbet Scraper\"}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(webhookUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payload delivered successfully, code " + response.StatusCode + ".");
            }
            else
            {
                Console.WriteLine($"Failed to send message. Status code: {response.StatusCode}");
            }
        }
    }
}