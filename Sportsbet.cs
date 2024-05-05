using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Project_Arbitrage
{
    internal class Sportsbet
    {
        private readonly HttpClient _client;
        private const string WebsiteUrl = "https://www.sportsbet.com.au/betting/australian-rules/afl"; // Link to SportsBet AFL matches page

        /**
         * Constructor
         * Initializes the HttpClient instance for making HTTP requests.
         */
        public Sportsbet()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /**
         * ScrapeData Method
         * Fetches data from the specified website URL and parses it.
         * Returns the extracted data as a string.
         */
        public async Task<string> ScrapeData()
        {
            try
            {
                var response = await _client.GetAsync(WebsiteUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Placeholder for data to be scraped
                var extractedData = "Data";

                return extractedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}