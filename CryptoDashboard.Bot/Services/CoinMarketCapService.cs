using CryptoDashboard.Bot.Dtos.CoinMarketCap;
using CryptoDashboard.Bot.Dtos.News;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CryptoDashboard.Bot.Services
{
    public class CoinMarketCapService
    {
        private readonly HttpClient _httpClient;

        public CoinMarketCapService(IHttpClientFactory clientFactory, IConfiguration configuration) {
            _httpClient = clientFactory.CreateClient("cmcClient");

        }

        public async Task<CryptoCurrencyInfoResponse> GetCurrencyInfo(string symbol)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "cryptocurrency/info");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["symbol"] = symbol;
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<CryptoCurrencyInfoResponse>(responseBody);
            return result;
        }

        public async Task<CryptoCurrencyInfoResponse> GetCurrenciesInfo(List<int> ids)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "cryptocurrency/info");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["id"] = string.Join(',', ids);
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<CryptoCurrencyInfoResponse>(responseBody);
            return result;
        }

    }
}
