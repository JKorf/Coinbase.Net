// 01-advanced-trade-market-and-account.cs
//
// Demonstrates: Coinbase Advanced Trade public market data, Coinbase Exchange
// market data, and credential-gated account balances.
//
// Setup: dotnet add package Coinbase.Net

using Coinbase.Net;
using Coinbase.Net.Clients;
using Coinbase.Net.Enums;

var symbol = "ETH-USD";

// Public REST clients do not need credentials.
var publicClient = new CoinbaseRestClient();

var product = await publicClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync(symbol);
if (!product.Success)
{
    Console.WriteLine($"Product request failed: {product.Error}");
    return;
}

Console.WriteLine($"{product.Data.Symbol} last price: {product.Data.LastPrice}");

var candles = await publicClient.AdvancedTradeApi.ExchangeData.GetKlinesAsync(
    symbol,
    KlineInterval.OneMinute,
    limit: 5);

if (candles.Success)
{
    foreach (var candle in candles.Data)
        Console.WriteLine($"{candle.OpenTime:u} close={candle.ClosePrice} volume={candle.Volume}");
}
else
{
    Console.WriteLine($"Candle request failed: {candles.Error}");
}

var orderBook = await publicClient.AdvancedTradeApi.ExchangeData.GetOrderBookAsync(symbol, limit: 50);
if (orderBook.Success)
{
    var bestBid = orderBook.Data.Bids.FirstOrDefault();
    var bestAsk = orderBook.Data.Asks.FirstOrDefault();
    Console.WriteLine($"Best bid: {bestBid?.Price}, best ask: {bestAsk?.Price}");
}

// Coinbase.Net also exposes Coinbase Exchange API market-data endpoints.
var exchangeProducts = await publicClient.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (exchangeProducts.Success)
    Console.WriteLine($"Exchange API products returned: {exchangeProducts.Data.Length}");

var keyName = Environment.GetEnvironmentVariable("COINBASE_API_KEY_NAME");
var privateKey = Environment.GetEnvironmentVariable("COINBASE_API_PRIVATE_KEY")?.Replace("\\n", "\n");
if (string.IsNullOrWhiteSpace(keyName) || string.IsNullOrWhiteSpace(privateKey))
{
    Console.WriteLine("Skipping private account request; COINBASE_API_KEY_NAME and COINBASE_API_PRIVATE_KEY are not set.");
    return;
}

var privateClient = new CoinbaseRestClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials(keyName, privateKey);
});

var accounts = await privateClient.AdvancedTradeApi.Account.GetAccountsAsync(limit: 100);
if (!accounts.Success)
{
    Console.WriteLine($"Account request failed: {accounts.Error}");
    return;
}

foreach (var account in accounts.Data.Accounts.Take(10))
    Console.WriteLine($"{account.Asset}: available={account.AvailableBalance.Value}, hold={account.HoldBalance.Value}");
