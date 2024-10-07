
using Coinbase.Net.Clients;

// REST
var restClient = new CoinbaseRestClient();
var ticker = await restClient.AdvancedTradeApi.ExchangeData.GetSpotTickersAsync("ETH-USDT");
Console.WriteLine($"Rest client ticker price for ETH-USDT: {ticker.Data.List.First().LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new CoinbaseSocketClient();
var subscription = await socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync("ETH-USDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETH-USDT: {update.Data.LastPrice}");
});

Console.ReadLine();
