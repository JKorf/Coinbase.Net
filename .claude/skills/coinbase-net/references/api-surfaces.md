# API surfaces

## Client roots and symbols

`client` below is a `CoinbaseRestClient`; `socketClient` is a `CoinbaseSocketClient`.

| Surface | Purpose |
| --- | --- |
| `client.AdvancedTradeApi.ExchangeData` | Products, books, candles, trades, prices, assets, exchange rates |
| `client.AdvancedTradeApi.Account` | Accounts, portfolios, derivatives balances/settings, fees, key permissions, and Coinbase App payment/deposit/withdrawal operations |
| `client.AdvancedTradeApi.Trading` | Orders, fills, position closing, futures and perpetual positions |
| `client.ExchangeApi.ExchangeData` | Coinbase Exchange time, currencies, and products |
| `socketClient.AdvancedTradeApi` | Advanced Trade public and authenticated streams |
| `socketClient.ExchangeApi` | Coinbase Exchange public streams |

Native product IDs use dashes, for example `ETH-USD`, `BTC-USD`, and `ETH-USDT`. Select actual derivative product IDs from product metadata. REST candles use `KlineInterval`; product selection uses `SymbolType`. Coinbase App endpoints remain under `AdvancedTradeApi.Account`, with account UUIDs distinct from portfolio IDs.

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

## Shared API V2

Use `client.AdvancedTradeApi.SharedApi` and `socketClient.AdvancedTradeApi.SharedApi`. The DI aggregate `ICoinbaseSharedApiClient` exposes `AdvancedTradeRest` and `AdvancedTradeSocket`. There is no Exchange API Shared API surface.

| Intent | Representative capabilities |
| --- | --- |
| Symbols, ticker, and book ticker | `IGetSpotSymbolsRest`, `IGetFuturesSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetBookTickerRest` |
| Books, trades, and candles | `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetTradeHistoryRest`, `IGetKlinesRest` |
| Assets, balances, and fees | `IGetAllAssetsRest`, `IGetAssetRest`, `IGetBalancesRest`, `IGetFeesRest` |
| Deposits and withdrawals | `IGetDepositAddressesRest`, `IGetDepositHistoryRest`, `IGetWithdrawalHistoryRest`, `IWithdrawRest` |
| Spot orders | `IPlaceSpotOrderRest`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `ICancelSpotOrderRest`, history/trade and trigger-order capabilities |
| Futures orders and positions | `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `ICancelFuturesOrderRest`, `IGetPositionsRest`, `ICloseFullPositionRest`, history/trade and trigger-order capabilities |
| Public socket streams | `ISubscribeKlinesSocket`, `ISubscribeTickerSocket`, `ISubscribeTradesSocket` |
| Private socket streams | `ISubscribeSpotOrdersSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket` |

Shared sockets do not expose an order-book subscription capability or socket order placement. Use the native order-book stream or a maintained local book where required. Native account and derivatives features are broader than the shared interfaces.

For dynamic selection, call the aggregate's `GetCapability` with a typed descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest` and the required trading mode. Check for a missing match before accessing `Capability` and `Options`. Use `GetCapabilities` for matching implementations and `Discover()` for operation metadata. With multiple exchanges registered, resolve `IEnumerable<TCapability>` and select by exchange and supported mode.

### Catalogs, quantities, and prerequisites

Use `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` and `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())`. After a successful request, read `SpotSymbolCatalog` or `FuturesSymbolCatalog`. Requests support base/quote asset type and subtype filters; results provide `DisplayName`, trading constraints, and classifications (`Crypto`, `Fiat`, `TradFi`; applicable `StableCoin`, `Equity`, `Commodity` subtypes). Shared trading modes include spot, perpetual linear, and delivery linear as supported by the chosen operation. Futures symbol retrieval defaults to perpetual products; explicitly select `TradingMode.DeliveryLinear` when requesting expiring futures. A successful catalog population does not imply every product family was fetched. Follow the account endpoint's pagination cursor when collecting all accounts.

Quantity-bearing shared models use `SharedOrderQuantity`. Spot limit orders and market sells use base quantity; spot market buys support base or quote quantities. Futures placement capabilities use contract quantities through `QuantityInContracts`. Shared order books report `SharedQuantityType.BaseAsset`. Inspect each capability's quantity options rather than assuming order-book and order quantities have the same units.

Perpetual balance and position requests require `PortfolioId` in `ExchangeParameters`. Deposit/address/withdrawal workflows can accept `AccountId`; when the library cannot resolve the asset account, provide it explicitly. Read the selected capability's exchange parameter rules rather than substituting a portfolio ID for an account ID.

Shared futures placement does not support `PositionSide`, inline `StopLossPrice`, or inline `TakeProfitPrice`. Reduce-only futures placement is supported through the market close-position path; non-market reduce-only requests are rejected. Use the native endpoints for Coinbase-specific order configurations and portfolio operations.

Shared REST returns `HttpResult<T>` / `HttpResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`.

## Environments and source navigation

`CoinbaseEnvironment.Live` is the built-in environment. `CreateCustom` requires Advanced Trade REST, Exchange REST, Advanced Trade public/private socket, and Exchange public socket addresses. A custom configuration does not itself supply a testnet.

Checkout-only: [public interfaces](../../../../Coinbase.Net/Interfaces/Clients/), [authentication provider](../../../../Coinbase.Net/CoinbaseAuthenticationProvider.cs), [local order books](../../../../Coinbase.Net/SymbolOrderBooks/), and [tracker factory](../../../../Coinbase.Net/CoinbaseTrackerFactory.cs).
