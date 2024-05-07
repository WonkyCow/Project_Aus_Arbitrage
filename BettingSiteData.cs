using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Arbitrage
{
    public class BettingSiteData
    {
        public string GameKey { get; set; }
        public string GameName { get; set; }
        public string SiteName { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public decimal Team1Price { get; set; }
        public decimal Team2Price { get; set; }
    }

}
