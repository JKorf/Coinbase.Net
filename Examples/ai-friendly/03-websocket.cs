// 03-websocket.cs
//
// Demonstrates: Coinbase Advanced Trade public streams, optional private user
// stream, and subscription teardown.
//
// Setup: dotnet add package Coinbase.Net

using Coinbase.Net;
using Coinbase.Net.Clients;

var symbol = "ETH-USD";
var socketClient = new CoinbaseSocketClient();

var tickerSubscription = await socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync(
    symbol,
    update => Console.WriteLine($"{update.Data.Symbol}: {update.Data.LastPrice}"));

if (!tickerSubscription.Success)
{
    Console.WriteLine($"Ticker subscription failed: {tickerSubscription.Error}");
    return;
}

var orderBookSubscription = await socketClient.AdvancedTradeApi.SubscribeToOrderBookUpdatesAsync(
    symbol,
    update => Console.WriteLine($"Book update: bids={update.Data.Bids.Length}, asks={update.Data.Asks.Length}"));

if (!orderBookSubscription.Success)
{
    Console.WriteLine($"Order book subscription failed: {orderBookSubscription.Error}");
    await socketClient.UnsubscribeAsync(tickerSubscription.Data);
    return;
}

var keyName = Environment.GetEnvironmentVariable("COINBASE_API_KEY_NAME");
var privateKey = Environment.GetEnvironmentVariable("COINBASE_API_PRIVATE_KEY")?.Replace("\\n", "\n");
CoinbaseSocketClient? privateSocketClient = null;

if (!string.IsNullOrWhiteSpace(keyName) && !string.IsNullOrWhiteSpace(privateKey))
{
    privateSocketClient = new CoinbaseSocketClient(options =>
    {
        options.ApiCredentials = new CoinbaseCredentials(keyName, privateKey);
    });

    var userSubscription = await privateSocketClient.AdvancedTradeApi.SubscribeToUserUpdatesAsync(
        update => Console.WriteLine($"User update orders={update.Data.Orders.Length}"));

    if (!userSubscription.Success)
        Console.WriteLine($"User subscription failed: {userSubscription.Error}");
}

Console.WriteLine("Subscriptions are active briefly for demonstration.");
await Task.Delay(TimeSpan.FromSeconds(10));

await socketClient.UnsubscribeAsync(tickerSubscription.Data);
await socketClient.UnsubscribeAsync(orderBookSubscription.Data);

if (privateSocketClient is not null)
    await privateSocketClient.UnsubscribeAllAsync();

Console.WriteLine("Subscriptions stopped.");
