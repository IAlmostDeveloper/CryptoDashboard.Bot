using CryptoDashboard.Bot.Dtos;
using CryptoDashboard.Bot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoDashboard.Bot.Helpers
{
    public class TickerInfoHelper
    {
        private readonly BinanceService _binanceService;
        private readonly CoinMarketCapService _coinMarketCapService;
        public TickerInfoHelper(IServiceScopeFactory serviceScopeFactory) { 
        using var scope = serviceScopeFactory.CreateScope();
            _binanceService = scope.ServiceProvider.GetService<BinanceService>();
            _coinMarketCapService = scope.ServiceProvider.GetService<CoinMarketCapService>();
        }

        public async Task<TickerInfo> GetTickerInfo(string ticker)
        {
            //var cmcCoinInfo = await _coinMarketCapService.GetCurrencyInfo(ticker);

            var priceChange = await _binanceService.Get24hPriceChange(ticker.Trim());

            var tickerInfo = new TickerInfo
            {
                Name = priceChange.symbol,
                Price = priceChange.lastPrice,
                PriceChange24h = $"{priceChange.priceChangePercent}%"
            };

            return tickerInfo;
        }
    }
}
