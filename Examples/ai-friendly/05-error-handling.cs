// 05-error-handling.cs
//
// Demonstrates: consistent Coinbase.Net result handling for REST, order
// placement responses, and WebSocket subscriptions.
//
// Setup: dotnet add package Coinbase.Net

using Coinbase.Net.Clients;
using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;

var restClient = new CoinbaseRestClient();

var productResult = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD");
if (!TryGetRestData(productResult, out CoinbaseSymbol? product))
    return;

Console.WriteLine($"{product.Symbol}: {product.LastPrice}");

// This intentionally demonstrates order response handling without placing an
// order by default. Use the trading example for a guarded live workflow.
static bool TryHandleOrderResult(WebCallResult<CoinbaseOrderResult> result, out string? orderId)
{
    orderId = null;

    if (!result.Success)
    {
        Console.WriteLine($"Order request failed. Code={result.Error?.Code}, Message={result.Error?.Message}");
        return false;
    }

    if (!result.Data.Success)
    {
        Console.WriteLine($"Order rejected. Code={result.Data.ErrorResponse.ErrorCode}, Message={result.Data.ErrorResponse.Message}");
        return false;
    }

    orderId = result.Data.SuccessResponse.OrderId;
    return true;
}

var socketClient = new CoinbaseSocketClient();
var subscriptionResult = await socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync(
    "ETH-USD",
    update => Console.WriteLine($"Ticker update: {update.Data.LastPrice}"));

if (!TryGetSocketData(subscriptionResult, out UpdateSubscription? subscription))
    return;

await Task.Delay(TimeSpan.FromSeconds(5));
await socketClient.UnsubscribeAsync(subscription);

static bool TryGetRestData<T>(WebCallResult<T> result, out T data)
{
    if (result.Success)
    {
        data = result.Data;
        return true;
    }

    Console.WriteLine($"REST call failed. Code={result.Error?.Code}, Message={result.Error?.Message}");
    data = default!;
    return false;
}

static bool TryGetSocketData<T>(CallResult<T> result, out T data)
{
    if (result.Success)
    {
        data = result.Data;
        return true;
    }

    Console.WriteLine($"Socket call failed. Code={result.Error?.Code}, Message={result.Error?.Message}");
    data = default!;
    return false;
}
