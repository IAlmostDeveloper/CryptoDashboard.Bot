using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDashboard.Bot.Dtos.CoinMarketCap
{
    public record struct CryptoCurrencyInfoResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public string logo { get; set; }

    }
}
