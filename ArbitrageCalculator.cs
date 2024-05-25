using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project_Arbitrage
{
    public class ArbitrageCalculator
    {
        public string CalculateArbitrage(List<BettingSiteData> dataList)
        {
            Console.WriteLine("Calculating Arbitrage");
            // Assuming all entries in the list are for the same game
            var highestTeam1Price = dataList.Max(x => x.Team1Price);
            Console.WriteLine($"highestTeam1Price{highestTeam1Price}");//debug code
            var highestTeam2Price = dataList.Max(x => x.Team2Price);
            Console.WriteLine($"highestTeam2Price{highestTeam2Price}"); //debug code

            var arbitragePercentage = ((1 / highestTeam1Price) + (1 / highestTeam2Price)) * 100;
            Console.WriteLine($"arbitragePercentage: {arbitragePercentage}");

            if (arbitragePercentage > 100)
            {
                Console.WriteLine("No arbitrage detected");//debug code
                return "No arbitrage detected"; //debug to verify message is working as intended
                //return "";
            }
            else
            {
                // Find the site names for the highest prices
                var siteNameTeam1 = dataList.First(x => x.Team1Price == highestTeam1Price).SiteName;
                Console.WriteLine($"siteNameTeam1: {siteNameTeam1}");//debug code
                var siteNameTeam2 = dataList.First(x => x.Team2Price == highestTeam2Price).SiteName;
                Console.WriteLine($"siteNameTeam2: {siteNameTeam2}");//debug code

                // Assuming all entries have the same game details
                var gameDetails = dataList.First();
                Console.WriteLine($"gameDetails: {gameDetails}");//debug code

                string result = ($"**Arbitrage Alert:** Arbitrage detected for game between **{gameDetails.Team1}** and **{gameDetails.Team2}** on **{gameDetails.Date}** at **{gameDetails.Time}**. Odds are **${highestTeam1Price}** for **{gameDetails.Team1}** from **{siteNameTeam1}**, and **${highestTeam2Price}** for **{gameDetails.Team2}** from **{siteNameTeam2}**.");
                Console.Write( result );

                //return $"**Arbitrage Alert:** Arbitrage detected for game between **{gameDetails.Team1}** and **{gameDetails.Team2}** on **{gameDetails.Date}** at **{gameDetails.Time}**. Odds are **${highestTeam1Price}** for **{gameDetails.Team1}** from **{siteNameTeam1}**, and **${highestTeam2Price}** for **{gameDetails.Team2}** from **{siteNameTeam2}**.";
                return result;
            }
        }
    }
}
