using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Project_Arbitrage
{
    internal class Ladbrokes
    {
        private IWebDriver _driver;

        public Ladbrokes()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless"); // Run in headless mode
            _driver = new ChromeDriver(chromeOptions);
        }

        /**
         * ScrapeData task
         * Scrapes data from the Ladbrokes website
         * Gets the time, date, team names, and H2H team odds for the next game from Ladbrokes.
         */
        public async Task<BettingSiteData> ScrapeData()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Attempting to scrape data from Ladbrokes");
                Console.ForegroundColor = ConsoleColor.DarkRed;

                _driver.Navigate().GoToUrl("https://www.ladbrokes.com.au/sports/australian-rules/afl");

                // Locate the price elements by their data-automation-id
                List<IWebElement> priceElements = _driver.FindElements(By.CssSelector("[data-testid='price-button-odds']")).ToList();

                /**
                Console.WriteLine("List of all prices");
                foreach (var element in priceElements) //debug code
                {
                    Console.WriteLine(element.Text);
                }*/ //used for debug

                // Extract the price text for both teams
                string priceTeam1 = priceElements[0].Text.Trim(); // First element
                string priceTeam2 = priceElements[1].Text.Trim(); // Second element

                //Console.WriteLine($"Price for Team 1: {priceTeam1}, Price for Team 2: {priceTeam2}");//used for debug



                // Locate the team name elements by their data-automation-id
                List<IWebElement> teamNameElements = _driver.FindElements(By.CssSelector("[class='displayTitle']")).ToList();

                /**
                Console.WriteLine("List of all team names");
                foreach (var element in teamNameElements) //debug code
                {
                    Console.WriteLine(element.Text);
                }*/ //used for debug

                // Extract the team names
                string team1Name = teamNameElements[0].Text.Trim(); // First element
                string team2Name = teamNameElements[1].Text.Trim(); // Second element

                // Console.WriteLine($"Team 1: {team1Name}, Team 2: {team2Name}"); //used for debug



                // Locate the date element/s by their data-automation-id
                List<IWebElement> gameDateElements = _driver.FindElements(By.CssSelector("[class='sports-date-title__text']")).ToList();

                /**
                Console.WriteLine("List of all game dates");
                foreach (var element in gameDateElements) //debug code
                {
                    Console.WriteLine(element.Text);
                }*/ //used for debug

                //extract date
                string gameDate = gameDateElements[0].Text.Trim(); // Game Date/Time

                //Console.WriteLine($"Date Data: {gameDate}"); //used for debug



                // Locate the time/ element/s by their data-automation-id
                List<IWebElement> gameTimeElements = _driver.FindElements(By.CssSelector("[data-testid='countdown']")).ToList();

                /**
                Console.WriteLine("List of all game times");
                foreach (var element in gameTimeElements) //debug code
                {   
                    Console.WriteLine(element.Text);
                }*/ //used for debug

                string gameTime = "";

                //extract game time
                if (gameTimeElements.Count > 0)
                {
                    gameTime = gameTimeElements[0].Text.Trim(); // Game Time
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No game time found.");
                    Console.ForegroundColor = ConsoleColor.White;
                    // Handle the case where no game time is found
                    gameTime = "timeError";
                }

                // Console.WriteLine($"Time Data: {gameTime}"); //used for debug

                //fix the date/time to be standardised
                // Declare standardizedDate and standardizedTime outside the if block
                //left over code from Sportsbet, to be fixed later
                string standardizedDate = gameDate;
                string standardizedTime = gameTime;

                //Console.WriteLine($"Standardized date: {standardizedDate}"); //used for debug
                //Console.WriteLine($"Standardized time: {standardizedTime}"); //used for debug


                // Construct the notification message
                //old code - could still be used for debug
                //string message = $"**Ladbrokes Alert**: Prices detected for game between **{team1Name} and {team2Name}** on **{standardizedDate}** at **{standardizedTime}**. Odds are **{priceTeam1}** for {team1Name}, **{priceTeam2}** for {team2Name}.";
               

                //string gameName = ($"{team1Name} vs {team2Name}"); //currently unused

                string gameName = ($"{team1Name} vs {team2Name}"); //currently unused

                BettingSiteData ladbrokesData = new BettingSiteData
                {
                    GameKey = "1",
                    GameName = gameName,
                    SiteName = "Ladbrokes",
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
                Console.WriteLine("Successfully scraped data from Ladbrokes!");
                Console.ForegroundColor = ConsoleColor.White;
                return ladbrokesData;
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
