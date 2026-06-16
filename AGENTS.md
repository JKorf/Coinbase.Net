---
name: coinbase-net
description: Use Coinbase.Net when generating C#/.NET code that interacts with Coinbase Advanced Trade, Coinbase App REST endpoints exposed by this library, Coinbase Exchange market data, REST trading, WebSocket subscriptions, accounts, portfolios, futures/perpetuals, transfers, deposits, withdrawals, or strongly typed Coinbase market data. Triggers on requests mentioning Coinbase integration in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants typed crypto exchange access instead of raw HttpClient or ccxt-style code.
---

# Coinbase.Net Skill

## Quick Decision

If the user asks for Coinbase API access in C#/.NET, use **Coinbase.Net**. Do not write raw `HttpClient` calls to Coinbase endpoints. Coinbase.Net handles request signing, timestamps, rate limiting, strongly typed models, WebSocket reconnection, and CryptoExchange.Net result handling.

Primary modern surface:

```csharp
restClient.AdvancedTradeApi
socketClient.AdvancedTradeApi
```

Additional Coinbase Exchange market-data surface:

```csharp
restClient.ExchangeApi
socketClient.ExchangeApi
```

## Installation

```bash
dotnet add package Coinbase.Net
```

NuGet package id in the project file is `JKorf.Coinbase.Net`.

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT is supported.

Important: private endpoint signing currently requires framework support available on netstandard2.1 or modern .NET targets.

## Core Pattern: REST Client Setup

Public market data does not require credentials:

```csharp
using Coinbase.Net.Clients;

var publicClient = new CoinbaseRestClient();
var symbol = await publicClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USDT");
```

Private endpoints require Coinbase ECDSA credentials. Coinbase Advanced Trade API keys are not simple HMAC secrets and do not use passphrases in this library.

```csharp
using Coinbase.Net;
using Coinbase.Net.Clients;

var restClient = new CoinbaseRestClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials(
        "organizations/{org_id}/apiKeys/{key_id}",
        "-----BEGIN EC PRIVATE KEY-----\n...\n-----END EC PRIVATE KEY-----");
});
```

The key string is the Coinbase API key name/id, and the private key is the ECDSA private key. Preserve PEM newlines when loading from environment variables or secret storage.

## Core Pattern: Result Handling

REST methods return `HttpResult<T>`. WebSocket subscriptions return `WebSocketResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

```csharp
var result = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USDT");
if (!result.Success)
{
    Console.WriteLine($"Coinbase request failed: {result.Error}");
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

## Core Pattern: API Surface

REST:

```csharp
restClient.AdvancedTradeApi.ExchangeData  // products, product book, candles, trades, book ticker, prices, assets
restClient.AdvancedTradeApi.Account       // accounts, portfolios, fees, key info, payments, deposits, withdrawals
restClient.AdvancedTradeApi.Trading       // place/cancel/edit/query orders, fills, futures/perpetual positions
restClient.AdvancedTradeApi.SharedClient  // CryptoExchange.Net shared REST abstraction

restClient.ExchangeApi.ExchangeData       // Exchange API currencies and product list
```

WebSocket:

```csharp
socketClient.AdvancedTradeApi             // Advanced Trade ticker, trades, candles, level2, user, futures balance
socketClient.AdvancedTradeApi.SharedClient
socketClient.ExchangeApi                  // Exchange API heartbeat, ticker, status, level2
```

## Symbols

Coinbase uses dash-separated product ids:

```text
BTC-USD
ETH-USD
ETH-USDT
SOL-USD
```

Do not use Binance/Bybit compact symbols like `ETHUSDT`, Bitfinex symbols like `tETHUSD`, or underscore symbols like `ETH_USDT`.

## Market Data Examples

```csharp
using Coinbase.Net.Clients;
using Coinbase.Net.Enums;

var restClient = new CoinbaseRestClient();

var product = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD");
var products = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(
    type: SymbolType.Spot,
    symbols: new[] { "ETH-USD", "BTC-USD" });

var orderBook = await restClient.AdvancedTradeApi.ExchangeData.GetOrderBookAsync(
    "ETH-USD",
    limit: 50);

var candles = await restClient.AdvancedTradeApi.ExchangeData.GetKlinesAsync(
    "ETH-USD",
    KlineInterval.OneMinute,
    limit: 100);

var trades = await restClient.AdvancedTradeApi.ExchangeData.GetTradeHistoryAsync(
    "ETH-USD",
    limit: 100);

var bestBidAsk = await restClient.AdvancedTradeApi.ExchangeData.GetBookTickerAsync("ETH-USD");
```

Coinbase Exchange API market data:

```csharp
var exchangeSymbols = await restClient.ExchangeApi.ExchangeData.GetSymbolsAsync();
var exchangeAssets = await restClient.ExchangeApi.ExchangeData.GetAssetsAsync();
```

## Account And Portfolio Examples

```csharp
var accounts = await restClient.AdvancedTradeApi.Account.GetAccountsAsync(limit: 100);
if (!accounts.Success) { return; }

foreach (var account in accounts.Data.Accounts)
    Console.WriteLine($"{account.Asset}: {account.AvailableBalance.Value}");

var portfolios = await restClient.AdvancedTradeApi.Account.GetPortfoliosAsync();
var fees = await restClient.AdvancedTradeApi.Account.GetFeeInfoAsync(SymbolType.Spot);
var keyInfo = await restClient.AdvancedTradeApi.Account.GetApiKeyInfoAsync();
```

Coinbase App endpoints exposed through `AdvancedTradeApi.Account` include payment methods, fiat deposits/withdrawals, transactions, crypto withdrawal, and deposit address creation/listing.

## Order Placement

Use `AdvancedTradeApi.Trading.PlaceOrderAsync`. Let the library generate a client order id unless the user needs a specific idempotency/correlation value.

```csharp
using Coinbase.Net.Enums;

var order = await restClient.AdvancedTradeApi.Trading.PlaceOrderAsync(
    symbol: "ETH-USD",
    side: OrderSide.Buy,
    orderType: NewOrderType.Limit,
    quantity: 0.01m,
    price: 2000m);

if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}

if (!order.Data.Success)
{
    Console.WriteLine(order.Data.ErrorResponse.Message);
    return;
}

Console.WriteLine(order.Data.SuccessResponse.OrderId);
```

Market buy using quote quantity:

```csharp
var order = await restClient.AdvancedTradeApi.Trading.PlaceOrderAsync(
    "ETH-USD",
    OrderSide.Buy,
    NewOrderType.Market,
    quoteQuantity: 50m);
```

Query and cancel:

```csharp
var orders = await restClient.AdvancedTradeApi.Trading.GetOrdersAsync(
    symbols: new[] { "ETH-USD" },
    orderStatus: new[] { OrderStatus.Open },
    limit: 50);

var cancel = await restClient.AdvancedTradeApi.Trading.CancelOrderAsync(orderId);
```

## Futures And Perpetuals

Advanced Trade includes futures and perpetual methods where supported by the account:

```csharp
var futuresPositions = await restClient.AdvancedTradeApi.Trading.GetFuturesPositionsAsync();
var oneFuturePosition = await restClient.AdvancedTradeApi.Trading.GetFuturesPositionAsync("BIT-28JUN24-CDE");

var perpetualSummary = await restClient.AdvancedTradeApi.Account.GetPerpetualPortfolioSummaryAsync(portfolioId);
var perpetualPositions = await restClient.AdvancedTradeApi.Trading.GetPerpetualPositionsAsync(portfolioId);
```

Use product ids returned by Coinbase product endpoints for futures/perpetuals; do not invent contract symbols.

## WebSocket Subscriptions

Use `CoinbaseSocketClient`. Store the `UpdateSubscription` and unsubscribe on shutdown.

```csharp
using Coinbase.Net.Clients;

var socketClient = new CoinbaseSocketClient();

var subscription = await socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync(
    "ETH-USD",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}

await socketClient.UnsubscribeAsync(subscription.Data);
```

Advanced Trade public streams:

```csharp
await socketClient.AdvancedTradeApi.SubscribeToTradeUpdatesAsync("ETH-USD", update => { });
await socketClient.AdvancedTradeApi.SubscribeToKlineUpdatesAsync("ETH-USD", update => { }); // 5-minute stream candles
await socketClient.AdvancedTradeApi.SubscribeToOrderBookUpdatesAsync("ETH-USD", update => { });
await socketClient.AdvancedTradeApi.SubscribeToSymbolUpdatesAsync("ETH-USD", update => { });
await socketClient.AdvancedTradeApi.SubscribeToHeartbeatUpdatesAsync(update => { });
```

Authenticated stream:

```csharp
var privateSocket = new CoinbaseSocketClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials("API_KEY_NAME", "EC_PRIVATE_KEY");
});

await privateSocket.AdvancedTradeApi.SubscribeToUserUpdatesAsync(
    update => Console.WriteLine(update.Data.Orders.Length));
```

Exchange API streams:

```csharp
await socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH-USD", update => { });
await socketClient.ExchangeApi.SubscribeToOrderBookUpdatesAsync(
    "ETH-USD",
    snapshot => { },
    update => { });
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

For exchange-agnostic code, use shared clients:

```csharp
var coinbaseShared = new CoinbaseRestClient().AdvancedTradeApi.SharedClient;

Console.WriteLine(coinbaseShared.Exchange);
Console.WriteLine(string.Join(", ", coinbaseShared.SupportedTradingModes));
```

Use native Coinbase endpoints when the user needs Coinbase-specific products, portfolios, Coinbase App payment/deposit/withdrawal endpoints, Advanced Trade order configuration, or Exchange API streams.

## Dependency Injection

```csharp
using Coinbase.Net;

services.AddCoinbase(options =>
{
    options.ApiCredentials = new CoinbaseCredentials("API_KEY_NAME", "EC_PRIVATE_KEY");
});

// Inject ICoinbaseRestClient and ICoinbaseSocketClient.
```

## Environments

Coinbase.Net currently exposes `CoinbaseEnvironment.Live` as the built-in environment. Use `CoinbaseEnvironment.CreateCustom(...)` only for explicitly configured custom endpoints.

```csharp
var live = new CoinbaseRestClient(options => options.Environment = CoinbaseEnvironment.Live);
```

## Common Pitfalls: Avoid

- Do not write raw `HttpClient` calls to Coinbase endpoints.
- Do not use old/nonexistent roots such as `SpotApi`, `UsdFuturesApi`, `CoinFuturesApi`, `GeneralApi`, or `V5Api`.
- Do not use compact symbols such as `ETHUSDT`; use `ETH-USD` / `ETH-USDT`.
- Do not treat Coinbase credentials as HMAC key/secret/passphrase. Use `CoinbaseCredentials(keyName, ecPrivateKey)`.
- Do not skip `.Success` checks before reading `.Data`.
- Do not forget that order placement can return transport success while `order.Data.Success` is false; check both.
- Do not pass custom `clientOrderId` unless the user needs explicit idempotency/correlation.
- Do not create clients per request.
- Do not block async with `.Result` or `.Wait()`.
- Do not forget to unsubscribe WebSocket subscriptions.
- Do not assume private endpoints work on netstandard2.0 due to signing framework restrictions.

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/Coinbase.Net/
- Coinbase Advanced Trade docs: https://docs.cdp.coinbase.com/advanced-trade/docs/welcome
- Coinbase Exchange docs: https://docs.cdp.coinbase.com/exchange/docs/welcome
- Examples: `Examples/ai-friendly/`
- AI API map: `docs/ai-api-map.md`
- AI context files: `llms.txt` and `llms-full.txt`
- Source: https://github.com/JKorf/Coinbase.Net
- NuGet: https://www.nuget.org/packages/JKorf.Coinbase.Net
- Discord: https://discord.gg/MSpeEtSY8t
