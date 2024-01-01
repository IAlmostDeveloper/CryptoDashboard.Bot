using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using CryptoDashboard.Bot.Extensions;
using Microsoft.Extensions.Configuration;
using CryptoDashboard.Bot.Services;
using CryptoDashboard.Bot.Commands;
using CryptoDashboard.Bot.Helpers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(x =>
    {
        x.AddUserSecrets<Program>();
        x.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        // Register Bot configuration
        services.Configure<BotConfiguration>(
            context.Configuration.GetSection(BotConfiguration.Configuration));

        // Register named HttpClient to benefits from IHttpClientFactory
        // and consume it with ITelegramBotClient typed client.
        // More read:
        //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
        //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
                    TelegramBotClientOptions options = new(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });
                
        services.AddHttpClient("newsClient", x =>
            {
                x.BaseAddress = new Uri("https://newsapi.org/v2/");
                x.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/666.0 (compatible; AcmeInc/6.0)");
            });

        services.AddHttpClient("cmcClient", x =>
            {
                x.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/v2/");
                x.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/123.0 (compatible; AcmeInc/1.0)");
                x.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", context.Configuration["CoinMarketCap:ApiKey"]);
            });

        services.AddHttpClient("binanceClient", x =>
        {
            x.BaseAddress = new Uri("https://api.binance.com/api/v3/");
            x.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/123.0 (compatible; AcmeInc/1.0)");
        });

        services.AddScoped<NewsService>();
        services.AddScoped<BinanceService>();
        services.AddScoped<CoinMarketCapService>();

        services.AddScoped<TickerInfoHelper>();

        services.AddBotCommands();

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();

#pragma warning disable CA1050 // Declare types in namespaces
#pragma warning disable RCS1110 // Declare type inside namespace.
public class BotConfiguration
#pragma warning restore RCS1110 // Declare type inside namespace.
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static readonly string Configuration = "BotConfiguration";

    public string BotToken { get; set; } = "";
}