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
            // Assuming all entries in the list are for the same game
            var highestTeam1Price = dataList.Max(x => x.Team1Price);
            var highestTeam2Price = dataList.Max(x => x.Team2Price);

            var arbitragePercentage = ((1 / highestTeam1Price) + (1 / highestTeam2Price)) * 100;

            if (arbitragePercentage > 100)
            {
                //return "No arbitrage detected"; //debug to verify message is working as intended
                return "";
            }
            else
            {
                // Find the site names for the highest prices
                var siteNameTeam1 = dataList.First(x => x.Team1Price == highestTeam1Price).SiteName;
                var siteNameTeam2 = dataList.First(x => x.Team2Price == highestTeam2Price).SiteName;

                // Assuming all entries have the same game details
                var gameDetails = dataList.First();

                return $"**Arbitrage Alert:** Arbitrage detected for game between **{gameDetails.Team1}** and **{gameDetails.Team2}** on **{gameDetails.Date}** at **{gameDetails.Time}**. Odds are **${highestTeam1Price}** for **{gameDetails.Team1}** from **{siteNameTeam1}**, and **${highestTeam2Price}** for **{gameDetails.Team2}** from **{siteNameTeam2}**.";
            }
        }
    }
}
