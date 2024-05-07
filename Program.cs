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
            string result="";
            string webhookUrl = "{Enter your Discord URL here!}";
            //string result = new Sportsbet().ScrapeData().Result; //old debug call

            var dataList = new List<BettingSiteData>();

            var sportsbetData = await new Sportsbet().ScrapeData();
            var ladbrokesData = await new Ladbrokes().ScrapeData();

            dataList.Add(sportsbetData);
            dataList.Add(ladbrokesData);

            var calculator = new ArbitrageCalculator();
            result = calculator.CalculateArbitrage(dataList);

            try
            {
                if (!string.IsNullOrEmpty(result)) //sends data to discord if any was gathered
                {
                    await SendDiscordNotification(webhookUrl, result);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Arbitrage was detected! Check your Discord webhook channel for information.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No Arbitrage was detected.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                /**else //sends error message if no data was gathered
                {
                    Console.WriteLine("Error! No data was retrieved/or an unknown error occured!");
                    string errorMessage = "Error! No data was retrieved/or an unknown error occured!";
                    await SendDiscordNotification(webhookUrl, errorMessage);
                }*/ //debug code
            }
            catch
            {
                Console.WriteLine("Warning! Error! Unknown error occured in Task Main!");
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