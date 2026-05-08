# Copilot Instructions for Coinbase.Net

This repository is **Coinbase.Net**, a strongly typed C#/.NET client library for Coinbase Advanced Trade, selected Coinbase App REST endpoints, and Coinbase Exchange market data/WebSocket APIs. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes Coinbase.Net, follow these conventions.

## Use Coinbase.Net, not raw HTTP

Never generate raw `HttpClient` calls to Coinbase endpoints. Always use `CoinbaseRestClient` or `CoinbaseSocketClient` so signing, timestamps, rate limiting, response parsing, and reconnect behavior stay correct.

## Client setup

```csharp
using Coinbase.Net;
using Coinbase.Net.Clients;

var restClient = new CoinbaseRestClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials("API_KEY_NAME", "EC_PRIVATE_KEY");
});
```

For public market data only, credentials are optional:

```csharp
var publicClient = new CoinbaseRestClient();
```

## Result handling

REST methods return `WebCallResult<T>` and WebSocket subscription methods return `CallResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

Order placement also has an exchange-level success flag:

```csharp
var order = await restClient.AdvancedTradeApi.Trading.PlaceOrderAsync(...);
if (!order.Success) { return; }
if (!order.Data.Success) { return; }
```

## API structure

- `restClient.AdvancedTradeApi.ExchangeData` for products, candles, trades, order book, best bid/ask, prices, assets
- `restClient.AdvancedTradeApi.Account` for accounts, portfolios, fees, key info, payment methods, deposits, withdrawals, transfers
- `restClient.AdvancedTradeApi.Trading` for orders, fills, futures positions, perpetual positions
- `restClient.AdvancedTradeApi.SharedClient` for CryptoExchange.Net shared REST abstractions
- `restClient.ExchangeApi.ExchangeData` for Coinbase Exchange currencies and products
- `socketClient.AdvancedTradeApi` for Advanced Trade ticker, trades, candles, level2, user, futures balance streams
- `socketClient.ExchangeApi` for Exchange API heartbeat, ticker, status, level2 streams

## Symbols

Use Coinbase product ids such as `ETH-USD`, `BTC-USD`, and `ETH-USDT`. Do not use `ETHUSDT`, `ETH_USDT`, `ETH/USD`, or `tETHUSD`.

## Order placement

```csharp
var order = await restClient.AdvancedTradeApi.Trading.PlaceOrderAsync(
    "ETH-USD",
    OrderSide.Buy,
    NewOrderType.Limit,
    quantity: 0.01m,
    price: 2000m);
```

Let the library generate `clientOrderId` unless the user needs explicit idempotency or external correlation.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown:

```csharp
var sub = await socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync(
    "ETH-USD",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success) { return; }
await socketClient.UnsubscribeAsync(sub.Data);
```

## Cross-exchange

For exchange-agnostic code, use `CryptoExchange.Net.SharedApis` through `.SharedClient`, for example `new CoinbaseRestClient().AdvancedTradeApi.SharedClient`.

## Avoid

- Raw Coinbase URLs and hand-written signing
- Nonexistent roots such as `SpotApi`, `UsdFuturesApi`, `CoinFuturesApi`, `GeneralApi`, or `V5Api`
- HMAC/passphrase credential examples
- Synchronous `.Result` / `.Wait()`
- Creating clients per request
- Reading `.Data` before checking `.Success`
- Ignoring `CoinbaseOrderResult.Success`
- Non-Coinbase symbol formats

## Reference

For detailed patterns and pitfalls see `CLAUDE.md`, `llms.txt`, `llms-full.txt`, and `docs/ai-api-map.md` in the repository root. Compilable examples live in `Examples/ai-friendly/`.
