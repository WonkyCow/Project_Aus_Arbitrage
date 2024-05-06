using System;
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

        public async Task<string> ScrapeData()
        {
            try
            {
                Console.WriteLine("Attempting to scrape data from Sportsbet");
                _driver.Navigate().GoToUrl("https://www.sportsbet.com.au/betting/australian-rules/afl");

                // Wait for the page to load
                // This might involve waiting for a specific element to appear or a certain condition to be met
                // WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                // wait.Until(ExpectedConditions.ElementExists(By.Id("dataLoadedElementId"))); // Replace with actual ID or selector

                // Locate the span element using a CSS selector
                IWebElement priceElement = _driver.FindElement(By.CssSelector(".size14_f7opyze.bold_f1au7gae.priceTextSize_frw9zm9"));

                Console.Write("Data extracted:" + priceElement.Text);

                // Extract the price text
                string price = priceElement.Text.Trim();

                Console.WriteLine("Price extract attempted, price gathered was:" + price);

                return price;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
            finally
            {
                _driver.Quit(); // Close the browser window
            }
        }
    }
}