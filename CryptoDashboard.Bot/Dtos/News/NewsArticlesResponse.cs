namespace CryptoDashboard.Bot.Dtos.News
{
    public record struct NewsArticlesResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<NewsArticle> Articles { get; set; }
    }
}
