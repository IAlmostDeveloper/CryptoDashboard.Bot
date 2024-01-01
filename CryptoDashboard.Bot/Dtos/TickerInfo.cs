namespace CryptoDashboard.Bot.Dtos
{
    public record struct TickerInfo
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string PriceChange24h { get; set; }

        public override string ToString()
        {
            return $"<b>Coin name:</b> {Name}\n<b>Current price:</b> {Price}\n<b>Price delta (24h):</b> {PriceChange24h.Substring(0, 4)}%";
        }
    }
}
