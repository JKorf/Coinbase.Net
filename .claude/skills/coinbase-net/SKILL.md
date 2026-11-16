---
name: coinbase-net
description: Implement or review Coinbase.Net Advanced Trade market data, accounts, portfolios, spot and derivatives trading, Coinbase App account operations, Exchange market data, WebSocket subscriptions, authentication, Shared API integration, and dependency injection in .NET applications.
---
# Coinbase.Net

Install with `dotnet add package JKorf.Coinbase.Net`.

- Reuse `CoinbaseRestClient` and `CoinbaseSocketClient`, or use dependency injection. `AdvancedTradeApi` is the primary trading surface; `ExchangeApi` provides separate Coinbase Exchange market-data endpoints and streams.
- Private operations use `CoinbaseCredentials(keyName, ecPrivateKey)`, without a HMAC secret or passphrase. Preserve EC PEM newlines. Authentication is unavailable in the library's `netstandard2.0` build; use a compatible modern .NET or `netstandard2.1` asset for signing.
- Native product IDs use dashes, such as `ETH-USD`. Discover actual futures/perpetual product IDs and account/portfolio IDs rather than constructing them.
- REST returns `HttpResult<T>` / `HttpResult`; subscriptions return `WebSocketResult<UpdateSubscription>`. Check `Success` before reading `Data` and handle `Error` on failure. Response shapes vary: some methods return arrays and others wrap account, trade, or pagination data.
- Native order placement also exposes `Data.Success`. Check it before accessing `SuccessResponse.OrderId`; inspect `ErrorResponse` on rejection. The library generates a client-order ID when omitted; use an explicit ID when the workflow needs durable correlation.
- Keep the concrete socket client and unsubscribe successful subscriptions on shutdown. Advanced Trade socket candles are fixed at five minutes; use REST for other intervals.
- Use `AdvancedTradeApi.SharedApi` for Shared API V2 and depend on the required capability interface. The aggregate exposes Advanced Trade REST/socket surfaces; Exchange API streams remain native.

## References

- Read [API surfaces](references/api-surfaces.md) for routing, Shared API capabilities, symbol catalogs, quantities, and account/portfolio prerequisites.
- Read [usage](references/usage.md) for authentication, market/account reads, order results, subscriptions, Shared API V2, and DI snippets.
- Read [operational considerations](references/safety.md) for nested outcomes, submission recovery, derivatives, portfolios, and fund movements.

Checkout-only: [repository instructions](../../../AGENTS.md) and [complete examples](../../../Examples/ai-friendly/).
