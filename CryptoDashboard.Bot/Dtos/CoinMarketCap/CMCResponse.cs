using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDashboard.Bot.Dtos.CoinMarketCap
{
    public record struct CMCResponse<T>
    {
        public T data {  get; set; }

        public string timestamp { get; set; }

        [JsonProperty(PropertyName = "error_code")]
        public int errorCode { get; set; }

        [JsonProperty(PropertyName = "error_message")]
        public string errorMessage { get; set; }
        public string elapsed { get; set; }

        [JsonProperty(PropertyName = "credit_count")]
        public string creditCount { get; set; }
        public string notice { get; set; }

    }
}
