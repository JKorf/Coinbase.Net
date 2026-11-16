# Usage examples

Snippets are independent. Those using `client` assume a reused `CoinbaseRestClient`, configured with credentials for private operations. Orders and cancellations change live account state; see [operational considerations](safety.md).

## Imports

```csharp
using System;
using System.Threading.Tasks;
using Coinbase.Net;
using Coinbase.Net.Clients;
using Coinbase.Net.Enums;
using Coinbase.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
```

## Public product and results

```csharp
using var client = new CoinbaseRestClient();
var product = await client.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD");
if (!product.Success)
{
    Console.WriteLine(product.Error);
    return;
}
Console.WriteLine(product.Data.LastPrice);
```

## ECDSA authentication and accounts

Load the key name and EC private key from configuration. Normalize escaped newlines only when the stored value contains literal `\n` sequences.

```csharp
var keyName = Environment.GetEnvironmentVariable("COINBASE_API_KEY_NAME")!;
var privateKey = Environment.GetEnvironmentVariable("COINBASE_PRIVATE_KEY")!
    .Replace("\\n", "\n");
using var client = new CoinbaseRestClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials(keyName, privateKey);
});
var accounts = await client.AdvancedTradeApi.Account.GetAccountsAsync(limit: 100);
if (!accounts.Success)
{
    Console.WriteLine(accounts.Error);
    return;
}
foreach (var account in accounts.Data.Accounts)
    Console.WriteLine($"{account.Asset}: {account.AvailableBalance.Value}");
```

## Spot limit order and cancellation

```csharp
var order = await client.AdvancedTradeApi.Trading.PlaceOrderAsync(
    "ETH-USD", OrderSide.Buy, NewOrderType.Limit, quantity: 0.01m, price: 1000m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
if (!order.Data.Success)
{
    Console.WriteLine(order.Data.ErrorResponse?.Message);
    return;
}
var orderId = order.Data.SuccessResponse.OrderId;
var cancel = await client.AdvancedTradeApi.Trading.CancelOrderAsync(orderId);
if (!cancel.Success)
    Console.WriteLine(cancel.Error);
```

## Futures position reads

```csharp
var positions = await client.AdvancedTradeApi.Trading.GetFuturesPositionsAsync();
if (!positions.Success)
{
    Console.WriteLine(positions.Error);
    return;
}
foreach (var position in positions.Data)
    Console.WriteLine($"{position.Symbol}: {position.PositionSide}");
```

Perpetual position requests instead require a portfolio ID. Select current products from symbol metadata; do not reuse expired contract IDs from examples.

## Subscription lifecycle

```csharp
using var socketClient = new CoinbaseSocketClient();
var subscription = await socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync(
    "ETH-USD", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Private user/futures-balance streams require credentials configured on the socket client. Coinbase Exchange subscriptions use `socketClient.ExchangeApi`; its level2 method takes separate snapshot and update callbacks.

## Shared API V2

```csharp
IGetTickerRest ticker = client.AdvancedTradeApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USD");
var result = await ticker.GetTickerAsync(new GetTickerRequest(symbol));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

See [API surfaces](api-surfaces.md#shared-api-v2) for symbol catalogs and account/portfolio prerequisites.

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddCoinbase(options =>
{
    options.ApiCredentials = new CoinbaseCredentials("API_KEY_NAME", "EC_PRIVATE_KEY");
});
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<ICoinbaseRestClient>();
var shared = provider.GetRequiredService<ICoinbaseSharedApiClient>();
```

## Complete examples (checkout-only)

The documentation test compiles these programs without executing exchange calls. Running them contacts Coinbase; review live-order configuration and account operations first.

- [Advanced Trade market and account](../../../../Examples/ai-friendly/01-advanced-trade-market-and-account.cs)
- [Advanced Trade trading](../../../../Examples/ai-friendly/02-advanced-trade-trading.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Shared API](../../../../Examples/ai-friendly/04-shared-client.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)
