namespace CryptoDashboard.Bot.Dtos.News
{
    public record struct NewsArticle
    {
        public NewsSource Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public string PublishedAt { get; set; }
    }
}
