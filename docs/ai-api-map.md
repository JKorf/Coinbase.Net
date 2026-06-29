# Coinbase.Net AI API Quick Map

Use this file to route common user intents to the correct Coinbase.Net client member. If a method name or parameter is not listed here, inspect `Coinbase.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new CoinbaseRestClient()` |
| WebSocket streams | `new CoinbaseSocketClient()` |
| Advanced Trade REST | `client.AdvancedTradeApi` |
| Coinbase Exchange REST market data | `client.ExchangeApi` |
| Advanced Trade WebSocket | `socketClient.AdvancedTradeApi` |
| Coinbase Exchange WebSocket | `socketClient.ExchangeApi` |
| API key authentication | `options.ApiCredentials = new CoinbaseCredentials("key-name", "ec-private-key")` |
| Live environment | `CoinbaseEnvironment.Live` |
| Custom environment | `CoinbaseEnvironment.CreateCustom(...)` |
| Dependency injection | `services.AddCoinbase(options => { ... })` |

Coinbase credentials are ECDSA key-name/private-key credentials. Do not generate HMAC secret/passphrase examples.

## Symbols

| User intent | Coinbase.Net pattern |
|---|---|
| Spot BTC/USD product id | `BTC-USD` |
| Spot ETH/USD product id | `ETH-USD` |
| Spot ETH/USDT product id | `ETH-USDT` |
| Discover supported product ids | `client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(...)` |
| Avoid Binance/Bybit compact symbols | Do not use `ETHUSDT` or `BTCUSDT` |
| Avoid non-Coinbase separators | Do not use `ETH_USDT`, `ETH/USD`, or `tETHUSD` |

## Advanced Trade Market Data REST

| User intent | Coinbase.Net member |
|---|---|
| Get server time | `client.AdvancedTradeApi.ExchangeData.GetServerTimeAsync()` |
| Get all products | `client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync()` |
| Get spot products | `client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(SymbolType.Spot)` |
| Get futures products | `client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(SymbolType.Futures)` |
| Get selected products | `client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(symbols: new[] { "ETH-USD" })` |
| Get one product/ticker | `client.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD")` |
| Get order book | `client.AdvancedTradeApi.ExchangeData.GetOrderBookAsync("ETH-USD", limit: 50)` |
| Get klines/candles | `client.AdvancedTradeApi.ExchangeData.GetKlinesAsync("ETH-USD", KlineInterval.OneMinute)` |
| Get recent public trades | `client.AdvancedTradeApi.ExchangeData.GetTradeHistoryAsync("ETH-USD")` |
| Get best bid/ask for one product | `client.AdvancedTradeApi.ExchangeData.GetBookTickerAsync("ETH-USD")` |
| Get best bid/ask for multiple products | `client.AdvancedTradeApi.ExchangeData.GetBookTickersAsync(new[] { "ETH-USD", "BTC-USD" })` |
| Get fiat currencies | `client.AdvancedTradeApi.ExchangeData.GetFiatAssetsAsync()` |
| Get crypto currencies | `client.AdvancedTradeApi.ExchangeData.GetCryptoAssetsAsync()` |
| Get exchange rates | `client.AdvancedTradeApi.ExchangeData.GetExchangeRatesAsync("USD")` |
| Get Coinbase App buy prices | `client.AdvancedTradeApi.ExchangeData.GetBuyPriceAsync("ETH-USD")` |
| Get Coinbase App sell prices | `client.AdvancedTradeApi.ExchangeData.GetSellPriceAsync("ETH-USD")` |
| Get Coinbase App spot prices | `client.AdvancedTradeApi.ExchangeData.GetSpotPriceAsync("ETH-USD")` |

## Advanced Trade Account REST

| User intent | Coinbase.Net member |
|---|---|
| Get accounts and balances | `client.AdvancedTradeApi.Account.GetAccountsAsync(limit: 100)` |
| Get one account | `client.AdvancedTradeApi.Account.GetAccountAsync(accountId)` |
| Get portfolios | `client.AdvancedTradeApi.Account.GetPortfoliosAsync()` |
| Get portfolios by type | `client.AdvancedTradeApi.Account.GetPortfoliosAsync(PortfolioType.Default)` |
| Get portfolio breakdown | `client.AdvancedTradeApi.Account.GetPortfolioAsync(portfolioId)` |
| Create portfolio | `client.AdvancedTradeApi.Account.CreatePortfolioAsync(portfolioName)` |
| Edit portfolio | `client.AdvancedTradeApi.Account.EditPortfolioAsync(portfolioId, newName)` |
| Delete portfolio | `client.AdvancedTradeApi.Account.DeletePortfolioAsync(portfolioId)` |
| Move funds between portfolios | `client.AdvancedTradeApi.Account.TransferPortfolioFundsAsync(...)` |
| Get fee tier info | `client.AdvancedTradeApi.Account.GetFeeInfoAsync(SymbolType.Spot)` |
| Get API key permissions | `client.AdvancedTradeApi.Account.GetApiKeyInfoAsync()` |
| Get payment methods | `client.AdvancedTradeApi.Account.GetPaymentMethodsAsync()` |
| Get payment method | `client.AdvancedTradeApi.Account.GetPaymentMethodAsync(paymentMethodId)` |
| Create convert quote | `client.AdvancedTradeApi.Account.CreateConvertQuoteAsync(fromAsset, toAsset, quantity)` |
| Get convert trade | `client.AdvancedTradeApi.Account.GetConvertTradeAsync(tradeId, fromAsset, toAsset)` |
| Commit convert trade | `client.AdvancedTradeApi.Account.CommitConvertTradeAsync(tradeId, fromAsset, toAsset)` |

## Coinbase App Account REST

Coinbase App deposit, withdrawal, transaction, and address endpoints are exposed under `AdvancedTradeApi.Account`, not under a separate `AppApi` root.

| User intent | Coinbase.Net member |
|---|---|
| Get fiat deposits | `client.AdvancedTradeApi.Account.GetDepositsAsync(accountId)` |
| Get one fiat deposit | `client.AdvancedTradeApi.Account.GetDepositAsync(accountId, depositId)` |
| Deposit fiat | `client.AdvancedTradeApi.Account.DepositAsync(accountId, paymentId, asset, quantity)` |
| Get fiat withdrawals | `client.AdvancedTradeApi.Account.GetWithdrawalsAsync(accountId)` |
| Get one fiat withdrawal | `client.AdvancedTradeApi.Account.GetWithdrawalAsync(accountId, withdrawalId)` |
| Withdraw fiat | `client.AdvancedTradeApi.Account.WithdrawAsync(accountId, asset, quantity, paymentMethod)` |
| Get transactions | `client.AdvancedTradeApi.Account.GetTransactionsAsync(accountId)` |
| Get one transaction | `client.AdvancedTradeApi.Account.GetTransactionAsync(accountId, transactionId)` |
| Get address transactions | `client.AdvancedTradeApi.Account.GetAddressTransactionsAsync(accountId, addressId)` |
| Withdraw crypto | `client.AdvancedTradeApi.Account.WithdrawCryptoAsync(accountId, to, quantity, asset, ...)` |
| Create deposit address | `client.AdvancedTradeApi.Account.CreateDepositAddressAsync(accountId, name)` |
| Get deposit addresses | `client.AdvancedTradeApi.Account.GetDepositAddressesAsync(accountId)` |
| Get one deposit address | `client.AdvancedTradeApi.Account.GetDepositAddressAsync(accountId, addressId)` |

## Futures And Perpetual Account REST

| User intent | Coinbase.Net member |
|---|---|
| Allocate portfolio funds | `client.AdvancedTradeApi.Account.AllocatePortfolioAsync(...)` |
| Get perpetual portfolio summary | `client.AdvancedTradeApi.Account.GetPerpetualPortfolioSummaryAsync(portfolioId)` |
| Get perpetual balances | `client.AdvancedTradeApi.Account.GetPerpetualBalancesAsync(portfolioId)` |
| Set perpetual multi-asset collateral mode | `client.AdvancedTradeApi.Account.SetPerpetualMultiAssetCollateralModeAsync(portfolioId, enabled)` |
| Get futures balance summary | `client.AdvancedTradeApi.Account.GetFuturesBalanceSummaryAsync()` |
| Set futures intraday margin setting | `client.AdvancedTradeApi.Account.SetFuturesIntradayMarginSettingAsync(setting)` |
| Get futures intraday margin setting | `client.AdvancedTradeApi.Account.GetFuturesIntradayMarginSettingAsync()` |
| Get futures current margin window | `client.AdvancedTradeApi.Account.GetFuturesCurrentMarginWindowAsync(marginProfileType)` |

## Advanced Trade Trading REST

| User intent | Coinbase.Net member |
|---|---|
| Place limit order | `client.AdvancedTradeApi.Trading.PlaceOrderAsync("ETH-USD", OrderSide.Buy, NewOrderType.Limit, quantity: 0.01m, price: 2000m)` |
| Place market buy with quote amount | `client.AdvancedTradeApi.Trading.PlaceOrderAsync("ETH-USD", OrderSide.Buy, NewOrderType.Market, quoteQuantity: 50m)` |
| Place market sell with base amount | `client.AdvancedTradeApi.Trading.PlaceOrderAsync("ETH-USD", OrderSide.Sell, NewOrderType.Market, quantity: 0.01m)` |
| Place post-only limit order | `client.AdvancedTradeApi.Trading.PlaceOrderAsync(..., postOnly: true)` |
| Place stop order | `client.AdvancedTradeApi.Trading.PlaceOrderAsync(..., stopPrice: price, stopDirection: StopDirection.StopUp)` |
| Cancel one order | `client.AdvancedTradeApi.Trading.CancelOrderAsync(orderId)` |
| Cancel multiple orders | `client.AdvancedTradeApi.Trading.CancelOrdersAsync(orderIds)` |
| Edit order | `client.AdvancedTradeApi.Trading.EditOrderAsync(orderId, price, quantity)` |
| Get one order | `client.AdvancedTradeApi.Trading.GetOrderAsync(orderId)` |
| Get orders | `client.AdvancedTradeApi.Trading.GetOrdersAsync(...)` |
| Get open orders for product | `client.AdvancedTradeApi.Trading.GetOrdersAsync(symbols: new[] { "ETH-USD" }, orderStatus: new[] { OrderStatus.Open })` |
| Get fills/user trades | `client.AdvancedTradeApi.Trading.GetUserTradesAsync(symbols: new[] { "ETH-USD" })` |
| Close position | `client.AdvancedTradeApi.Trading.ClosePositionAsync(symbol, quantity)` |
| Get futures positions | `client.AdvancedTradeApi.Trading.GetFuturesPositionsAsync()` |
| Get one futures position | `client.AdvancedTradeApi.Trading.GetFuturesPositionAsync(symbol)` |
| Get perpetual positions | `client.AdvancedTradeApi.Trading.GetPerpetualPositionsAsync(portfolioId)` |
| Get one perpetual position | `client.AdvancedTradeApi.Trading.GetPerpetualPositionAsync(portfolioId, symbol)` |

For order placement, check both `result.Success` and `result.Data.Success` before reading `SuccessResponse`.

## Coinbase Exchange REST

| User intent | Coinbase.Net member |
|---|---|
| Get Exchange API server time | `client.ExchangeApi.ExchangeData.GetServerTimeAsync()` |
| Get Exchange API currencies | `client.ExchangeApi.ExchangeData.GetAssetsAsync()` |
| Get Exchange API products | `client.ExchangeApi.ExchangeData.GetSymbolsAsync()` |

## Advanced Trade WebSocket

| User intent | Coinbase.Net member |
|---|---|
| Subscribe heartbeats | `socketClient.AdvancedTradeApi.SubscribeToHeartbeatUpdatesAsync(handler)` |
| Subscribe trades for one product | `socketClient.AdvancedTradeApi.SubscribeToTradeUpdatesAsync("ETH-USD", handler)` |
| Subscribe trades for many products | `socketClient.AdvancedTradeApi.SubscribeToTradeUpdatesAsync(symbols, handler)` |
| Subscribe 5-minute candles for one product | `socketClient.AdvancedTradeApi.SubscribeToKlineUpdatesAsync("ETH-USD", handler)` |
| Subscribe 5-minute candles for many products | `socketClient.AdvancedTradeApi.SubscribeToKlineUpdatesAsync(symbols, handler)` |
| Subscribe ticker updates | `socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync("ETH-USD", handler)` |
| Subscribe batched ticker updates | `socketClient.AdvancedTradeApi.SubscribeToBatchedTickerUpdatesAsync("ETH-USD", handler)` |
| Subscribe product/status updates | `socketClient.AdvancedTradeApi.SubscribeToSymbolUpdatesAsync("ETH-USD", handler)` |
| Subscribe order book updates | `socketClient.AdvancedTradeApi.SubscribeToOrderBookUpdatesAsync("ETH-USD", handler)` |
| Subscribe user order/position updates | `socketClient.AdvancedTradeApi.SubscribeToUserUpdatesAsync(handler)` |
| Subscribe futures balance updates | `socketClient.AdvancedTradeApi.SubscribeToFuturesBalanceUpdatesAsync(handler)` |

Advanced Trade kline WebSocket updates are always 5-minute candles; use REST `GetKlinesAsync` when a specific interval is needed.

## Coinbase Exchange WebSocket

| User intent | Coinbase.Net member |
|---|---|
| Subscribe Exchange heartbeat | `socketClient.ExchangeApi.SubscribeToHeartbeatUpdatesAsync("ETH-USD", handler)` |
| Subscribe Exchange status/product updates | `socketClient.ExchangeApi.SubscribeToExchangeInfoUpdatesAsync(handler)` |
| Subscribe Exchange ticker | `socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH-USD", handler)` |
| Subscribe Exchange ticker for many products | `socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync(symbols, handler)` |
| Subscribe Exchange batched ticker | `socketClient.ExchangeApi.SubscribeToBatchedTickerUpdatesAsync("ETH-USD", handler)` |
| Subscribe Exchange level2 order book | `socketClient.ExchangeApi.SubscribeToOrderBookUpdatesAsync("ETH-USD", onSnapshot, onUpdate)` |

## SharedApis

Use SharedApis for exchange-agnostic code across Coinbase, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

| User intent | Coinbase.Net member or interface |
|---|---|
| Shared Advanced Trade REST client | `new CoinbaseRestClient().AdvancedTradeApi.SharedClient` |
| Shared Advanced Trade socket client | `new CoinbaseSocketClient().AdvancedTradeApi.SharedClient` |
| Discover shared capabilities | `client.AdvancedTradeApi.SharedClient.Discover()` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared kline REST | `IKlineRestClient.GetKlinesAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared kline socket | `IKlineSocketClient.SubscribeToKlineUpdatesAsync(...)` |
| Shared trade socket | `ITradeSocketClient.SubscribeToTradeUpdatesAsync(...)` |
| Shared order socket | `ISpotOrderSocketClient` / `IFuturesOrderSocketClient` |
| Shared position socket | `IPositionSocketClient` |

Shared REST calls return `HttpResult<T>` / `HttpResult`. Shared socket subscriptions return `WebSocketResult<UpdateSubscription>`. Shared non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`.

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `WebSocketResult<UpdateSubscription> sub = await ...; if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Shared helper data | Read `ExchangeCallResult<T>.Data` only after `result.Success` |
| Place-order transport success | `if (!order.Success) { Console.WriteLine(order.Error); return; }` |
| Place-order exchange success | `if (!order.Data.Success) { Console.WriteLine(order.Data.ErrorResponse.Message); return; }` |
| Read placed order id | `order.Data.SuccessResponse.OrderId` only after both success checks |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Credential And Framework Pitfalls

| Do not use | Use instead |
|---|---|
| HMAC key/secret examples | `new CoinbaseCredentials(keyName, ecPrivateKey)` |
| API passphrase examples | No passphrase is used by `CoinbaseCredentials` |
| Flattened private key without newlines | Preserve PEM newlines or replace escaped `\\n` with `\n` |
| Private endpoints on netstandard2.0 | Use netstandard2.1 or modern .NET targets for signing support |
| Manual JWT/signing code | Let Coinbase.Net sign requests |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `CoinbaseClient` | `CoinbaseRestClient` |
| `SpotApi` | `AdvancedTradeApi` |
| `UsdFuturesApi` | `AdvancedTradeApi.Trading` / `AdvancedTradeApi.Account` futures methods |
| `CoinFuturesApi` | `AdvancedTradeApi.Trading` / `AdvancedTradeApi.Account` futures methods |
| `GeneralApi` | `AdvancedTradeApi.Account` for Coinbase App and portfolio endpoints |
| `V5Api` | `AdvancedTradeApi` / `ExchangeApi` |
| `ETHUSDT` | `ETH-USDT` or `ETH-USD` |
| `BTCUSDT` | `BTC-USD` or a listed Coinbase product id |
| `.Data` without `.Success` check | Check `.Success` first |
| `CoinbaseOrderResult.SuccessResponse` without checking `CoinbaseOrderResult.Success` | Check both success layers |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
| Custom `clientOrderId` by default | Let Coinbase.Net auto-generate it unless explicit idempotency is needed |

## Source Of Truth

| Need | Inspect |
|---|---|
| REST root shape | `Coinbase.Net/Interfaces/Clients/ICoinbaseRestClient.cs` |
| Socket root shape | `Coinbase.Net/Interfaces/Clients/ICoinbaseSocketClient.cs` |
| Advanced Trade market data methods | `Coinbase.Net/Interfaces/Clients/AdvancedTradeApi/ICoinbaseRestClientAdvancedTradeApiExchangeData.cs` |
| Advanced Trade account methods | `Coinbase.Net/Interfaces/Clients/AdvancedTradeApi/ICoinbaseRestClientAdvancedTradeApiAccount.cs` |
| Advanced Trade trading methods | `Coinbase.Net/Interfaces/Clients/AdvancedTradeApi/ICoinbaseRestClientAdvancedTradeApiTrading.cs` |
| Advanced Trade socket methods | `Coinbase.Net/Interfaces/Clients/AdvancedTradeApi/ICoinbaseSocketClientAdvancedTradeApi.cs` |
| Exchange API REST methods | `Coinbase.Net/Interfaces/Clients/ExchangeApi/ICoinbaseRestClientExchangeApiExchangeData.cs` |
| Exchange API socket methods | `Coinbase.Net/Interfaces/Clients/ExchangeApi/ICoinbaseSocketClientExchangeApi.cs` |
| Enums and valid enum names | `Coinbase.Net/Enums/*.cs` |
