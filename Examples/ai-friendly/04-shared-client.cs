// 04-shared-client.cs
//
// Demonstrates: accessing CryptoExchange.Net shared clients from Coinbase.Net
// while keeping native Coinbase endpoints available.
//
// Setup: dotnet add package Coinbase.Net

using Coinbase.Net.Clients;

var restClient = new CoinbaseRestClient();

var sharedRest = restClient.AdvancedTradeApi.SharedClient;
Console.WriteLine($"Shared exchange: {sharedRest.Exchange}");
Console.WriteLine($"Supported trading modes: {string.Join(", ", sharedRest.SupportedTradingModes)}");

var socketClient = new CoinbaseSocketClient();
Console.WriteLine($"Shared socket exchange: {socketClient.AdvancedTradeApi.SharedClient.Exchange}");

// Native Coinbase endpoints are still best when you need product metadata,
// portfolios, Coinbase App deposits/withdrawals, or Advanced Trade order fields.
var nativeProduct = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD");
if (nativeProduct.Success)
    Console.WriteLine($"Native Coinbase product: {nativeProduct.Data.Symbol} last={nativeProduct.Data.LastPrice}");

var exchangeProducts = await restClient.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (exchangeProducts.Success)
    Console.WriteLine($"Exchange API product count: {exchangeProducts.Data.Length}");
