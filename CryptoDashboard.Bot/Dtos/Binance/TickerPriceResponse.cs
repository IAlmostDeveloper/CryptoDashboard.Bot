namespace CryptoDashboard.Bot.Dtos.Binance
{
    public record struct TickerPriceResponse
    {
        public string symbol { get; set; }
        public string price { get; set; }
    }
}
