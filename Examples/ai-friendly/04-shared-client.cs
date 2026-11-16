// 04-shared-client.cs
//
// Demonstrates: accessing CryptoExchange.Net Shared API V2 capabilities from Coinbase.Net
// while keeping native Coinbase endpoints available.
//
// Setup: dotnet add package JKorf.Coinbase.Net

using Coinbase.Net.Clients;
using CryptoExchange.Net.SharedApis;

using var restClient = new CoinbaseRestClient();

var sharedRest = restClient.AdvancedTradeApi.SharedApi;
Console.WriteLine($"Shared exchange: {sharedRest.Exchange}");

var info = sharedRest.Discover();
var supportedFeatures = info.Features.Where(x => x.Supported).Select(x => x.EndpointName);
Console.WriteLine($"{info.Exchange} {info.TypeName}: {string.Join(", ", supportedFeatures)}");

using var socketClient = new CoinbaseSocketClient();
Console.WriteLine($"Shared socket exchange: {socketClient.AdvancedTradeApi.SharedApi.Exchange}");

// Use a narrow V2 capability and populate the normalized spot symbol catalog.
IGetSpotSymbolsRest symbolsClient = sharedRest;
var symbols = await symbolsClient.GetSpotSymbolsAsync(new GetSymbolsRequest());
if (!symbols.Success)
{
    Console.WriteLine(symbols.Error);
    return;
}
foreach (var symbol in symbols.Data.Take(5))
    Console.WriteLine($"{symbol.Name}: {symbol.BaseAsset}/{symbol.QuoteAsset}");
var catalog = symbolsClient.SpotSymbolCatalog;

// Native Coinbase endpoints are still best when you need product metadata,
// portfolios, Coinbase App deposits/withdrawals, or Advanced Trade order fields.
var nativeProduct = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD");
if (nativeProduct.Success)
    Console.WriteLine($"Native Coinbase product: {nativeProduct.Data.Symbol} last={nativeProduct.Data.LastPrice}");

var exchangeProducts = await restClient.ExchangeApi.ExchangeData.GetSymbolsAsync();
if (exchangeProducts.Success)
    Console.WriteLine($"Exchange API product count: {exchangeProducts.Data.Length}");
