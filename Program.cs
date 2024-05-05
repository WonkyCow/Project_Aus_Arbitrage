using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Project_Arbitrage
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();


        /**
         * Main Call function
         * Defines the information to be sent to the discord webhook URL, and calls the SendDiscordNotification
         */
        static async Task Main(string[] args)
        {
            string webhookUrl = "https://discord.com/api/webhooks/1236561732395077705/bNT7gn5pwS4KHBYwxaMResYgueYxUM02EL_8EI1KEold1K9r3rUgUax7uXfqj2PI9xn2";
            string sportsbet = new Sportsbet().ScrapeData().Result; // Assuming ScrapeData is an async method

            if (!string.IsNullOrEmpty(sportsbet)) //sends sportsbet data to discord if any was gathered
            {
                await SendDiscordNotification(webhookUrl, sportsbet);
            }
            else //sends error message if no data was gathered
            {
                Console.WriteLine("Error! No data was retrieved/or an unknown error occured!");
                string errorMessage = "Error! No data was retrieved/or an unknown error occured!";
                await SendDiscordNotification(webhookUrl, errorMessage);

            }
        }
        /**
         * Send Notification function
         * Sends a message to discord via a Discord webhook
         * Compiles information from the Main function into a HTTPs request
         */
        static async Task SendDiscordNotification(string webhookUrl, string message)
        {
            var content = new StringContent("{\"content\": \"" + message + "\", \"username\": \"Aussie Arbirage\"}", Encoding.UTF8, "application/json"); //http request to the Discord API/webhook
            var response = await client.PostAsync(webhookUrl, content);

            if (response.IsSuccessStatusCode) //returns request code to console
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