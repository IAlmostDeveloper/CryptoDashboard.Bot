using System.Web;
using CryptoDashboard.Bot.Dtos.News;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CryptoDashboard.Bot.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public NewsService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient("newsClient");
            _apiKey = configuration["NewsApi:ApiKey"];
        }

        public async Task<NewsArticlesResponse> GetTopHeadlines()
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "top-headlines");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["country"] = "ru";
            query["sortBy"] = "publishedAt";
            query["apiKey"] = _apiKey;
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<NewsArticlesResponse>(responseBody);
            return result;
        }

        public async Task<NewsArticlesResponse> GetArticlesByKeyword(string word)
        {
            var builder = new UriBuilder(_httpClient.BaseAddress + "everything");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["q"] = word;
            query["sortBy"] = "publishedAt";
            query["apiKey"] = _apiKey;
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NewsArticlesResponse>(responseBody);
        }
    }
}
