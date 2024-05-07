using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Project_Arbitrage
{
    internal class Sportsbet
    {
        private IWebDriver _driver;

        public Sportsbet()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless"); // Run in headless mode
            _driver = new ChromeDriver(chromeOptions);
        }

        /**
         * ScrapeData task
         * Scrapes data from the sportsbet website
         * Gets the time, date, team names, and H2H team odds for the next game from Sportsbet.
         */
        public async Task<BettingSiteData> ScrapeData()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Attempting to scrape data from Sportsbet");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                _driver.Navigate().GoToUrl("https://www.sportsbet.com.au/betting/australian-rules/afl");

                // Locate the price elements by their data-automation-id
                List<IWebElement> priceElements = _driver.FindElements(By.CssSelector("[data-automation-id='price-text']")).ToList();

                /**
                Console.WriteLine("List of all prices");
                foreach (var element in priceElements) //debug code
                {
                    Console.WriteLine(element.Text);
                }*/

                // Extract the price text for both teams
                string priceTeam1 = priceElements[0].Text.Trim(); // First element
                string priceTeam2 = priceElements[1].Text.Trim(); // Second element

                //Console.WriteLine($"Price for Team 1: {priceTeam1}, Price for Team 2: {priceTeam2}");

                // Locate the team name elements by their data-automation-id
                List<IWebElement> teamNameElements = _driver.FindElements(By.CssSelector("[data-automation-id='participant-one'], [data-automation-id='participant-two']")).ToList();

                /**
                Console.WriteLine("List of all team names");
                foreach (var element in teamNameElements) //debug code
                {
                    Console.WriteLine(element.Text);
                }*/

                // Extract the team names
                string team1Name = teamNameElements[0].Text.Trim(); // First element
                string team2Name = teamNameElements[1].Text.Trim(); // Second element

                //Console.WriteLine($"Team 1: {team1Name}, Team 2: {team2Name}");

                // Locate the time/date element/s by their data-automation-id
                List<IWebElement> gameDateTimeElements = _driver.FindElements(By.CssSelector("[data-automation-id='competition-event-card-time']")).ToList();

                /**
                Console.WriteLine("List of all game times/dates");
                foreach (var element in gameDateTimeElements) //debug code
                {
                    Console.WriteLine(element.Text);
                }*/

                //extract date/time
                string gameDateTime = gameDateTimeElements[0].Text.Trim(); // Game Date/Time

                //Console.WriteLine($"Time/Date Data: {gameDateTime}");

                // Declare standardizedDate and standardizedTime outside the if block
                string standardizedDate = "";
                string standardizedTime = "";

                // Assuming gameDateTime is the string from Sportsbet in the format "Day, Date Month 24hTime"
                DateTime gameDateTimeObj;
                if (DateTime.TryParse(gameDateTime, out gameDateTimeObj))
                {
                    // Format the DateTime object into the desired output format
                    standardizedDate = gameDateTimeObj.ToString("dd/MM/yyyy");
                    standardizedTime = gameDateTimeObj.ToString("HH:mm");
                }
                else
                {
                    // Attempt to parse using a specific format
                    string[] formats = { "dddd, d MMMM HH:mm", "dddd, d MMMM h:mm tt" };
                    if (DateTime.TryParseExact(gameDateTime, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out gameDateTimeObj))
                    {
                        // Successfully parsed with a specific format
                        standardizedDate = gameDateTimeObj.ToString("dd/MM/yyyy");
                        standardizedTime = gameDateTimeObj.ToString("HH:mm");
                    }
                    else
                    {
                        // Handle parsing failure
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to parse date and time from Sportsbet.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return null;
                    }
                }

                //Console.WriteLine($"standarised date: {standardizedDate}");
                //Console.WriteLine($"standarised time: {standardizedTime}");


                // Construct the notification message
                string message = $"**Sportsbet Alert**: Prices detected for game between **{team1Name} and {team2Name}** on **{standardizedDate}** at **{standardizedTime}**. Odds are **{priceTeam1}** for {team1Name}, **{priceTeam2}** for {team2Name}.";

                string gameName = ($"{team1Name} vs {team2Name}");              

                BettingSiteData sportsbetData = new BettingSiteData
                {
                    GameKey = "1",
                    GameName = gameName,
                    SiteName = "Sportsbet",
                    Time = standardizedTime, // Placeholder, replace with actual time
                    Date = standardizedDate, // Placeholder, replace with actual date
                    Team1 = team1Name, // Team 1 name gathered from sportsbet website
                    Team2 = team2Name, // Team 2 name gathered from sportsbet website
                    Team1Price = decimal.Parse(priceTeam1), // Convert string to decimal
                    //Team1Price = decimal.Parse("100.00"), // Test example to force an arbitrage
                    Team2Price = decimal.Parse(priceTeam2) // Convert string to decimal
                    //Team2Price = decimal.Parse("100.00"), // Test example to force an arbitrage

                };
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully scraped data from Sportsbet!");
                Console.ForegroundColor = ConsoleColor.White;

                return sportsbetData;
                //return message;

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            finally
            {
                _driver.Quit(); // Close the browser window
            }


        }
    }
}
