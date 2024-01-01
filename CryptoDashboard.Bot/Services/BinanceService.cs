using CryptoDashboard.Bot.Dtos.Binance;
using CryptoDashboard.Bot.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Web;

namespace CryptoDashboard.Bot.Services
{
    public class BinanceService
    {
        private readonly HttpClient _httpClient;
        public BinanceService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient("binanceClient");
        }

        public async Task<PriceChangeResponse> Get24hPriceChange(string symbol)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "ticker/24hr");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["symbol"] = symbol;
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<PriceChangeResponse>(responseBody);
            return result;
        }

        public async Task<PriceChangeResponse[]> Get24hPriceChange(List<string> symbols)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "ticker/24hr");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["symbols"] = symbols.ToArrayString();
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<PriceChangeResponse[]>(responseBody);
            return result;
        }

        public async Task<PriceChangeResponse[]> GetTickerPrice(List<string> symbols)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "ticker/price");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["symbols"] = symbols.ToArrayString();
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<PriceChangeResponse[]>(responseBody);
            return result;
        }

        public async Task<PriceChangeResponse[]> GetTickerPrice(string symbol)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "ticker/price");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["symbol"] = symbol;
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<PriceChangeResponse[]>(responseBody);
            return result;
        }

    }
}
