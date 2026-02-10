# ![Coinbase.Net](https://raw.githubusercontent.com/JKorf/Coinbase.Net/master/Coinbase.Net/Icon/icon.png) Coinbase.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Coinbase.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Coinbase.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Coinbase.Net?style=for-the-badge)

Coinbase.Net is a client library for accessing the [Coinbase Advanced Trade REST and Websocket API](https://docs.cdp.coinbase.com/advanced-trade/docs/welcome) and [Coinbase App REST API](https://docs.cdp.coinbase.com/coinbase-app/docs/welcome). 

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* High performance
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as the latest dotnet versions to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/Jkorf.Coinbase.net.svg?style=for-the-badge)](https://www.nuget.org/packages/Jkorf.Coinbase.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/Jkorf.Coinbase.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/Jkorf.Coinbase.Net)

	dotnet add package Coinbase.Net
	
### GitHub packages
Coinbase.Net is available on [GitHub packages](https://github.com/JKorf/Coinbase.Net/pkgs/nuget/Jkorf.Coinbase.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/Coinbase.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/Coinbase.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/Coinbase.Net/releases).

## How to use
* REST Endpoints
	```csharp
	// Get the ETH/USDT ticker via rest request
	var restClient = new CoinbaseRestClient();
	var tickerResult = await restClient.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USDT");
	var lastPrice = tickerResult.Data.LastPrice;
	```
* Websocket streams
	```csharp
	// Subscribe to ETH/USDT ticker updates via the websocket API
	var socketClient = new CoinbaseSocketClient();
	var tickerSubscriptionResult = socketClient.AdvancedTradeApi.SubscribeToTickerUpdatesAsync("ETHUSDT", (update) => 
	{
	  var lastPrice = update.Data.LastPrice;
	});
	```

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev?library=Coinbase.Net), or have a look at the examples [here](https://github.com/JKorf/Coinbase.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Coinbase.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Aster|[JKorf/Aster.Net](https://github.com/JKorf/Aster.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Aster.Net)|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.net.svg?style=flat-square)](https://www.nuget.org/packages/Jkorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Polymarket|[JKorf/Polymarket.Net](https://github.com/JKorf/Polymarket.Net)|[![Nuget version](https://img.shields.io/nuget/v/Polymarket.net.svg?style=flat-square)](https://www.nuget.org/packages/Polymarket.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

When using multiple of these API's the [CryptoClients.Net](https://github.com/JKorf/CryptoClients.Net) package can be used which combines this and the other packages and allows easy access to all exchange API's.

## Discord
[![Discord](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality

*Due to framework restrictions for signing requests only .netstandard 2.1 can currently use private endpoints*

### Advanced Trade REST
|API|Supported|Location|
|--|--:|--|
|Account|✓|`restClient.AdvancedTradeApi.Account`|
|Products|✓|`restClient.AdvancedTradeApi.ExchangeData`|
|Orders|✓|`restClient.AdvancedTradeApi.Trading`|
|Portfolios|✓|`restClient.AdvancedTradeApi.Account`|
|Futures|✓|`restClient.AdvancedTradeApi.Account`/`restClient.AdvancedTradeApi.Trading`|
|Perpetuals|✓|`restClient.AdvancedTradeApi.Account`/`restClient.AdvancedTradeApi.Trading`|
|Fees|✓|`restClient.AdvancedTradeApi.Account`|
|Convert|✓|`restClient.AdvancedTradeApi.Account`|
|Public|✓|`restClient.AdvancedTradeApi.ExchangeData`|
|Payment Methods|✓|`restClient.AdvancedTradeApi.Account`|
|Data API|✓|`restClient.AdvancedTradeApi.Account`|

### Advanced Trade Websocket
|API|Supported|Location|
|--|--:|--|
|All channels|✓|`socketClient.AdvancedTradeApi`|

### App
|API|Supported|Location|
|--|--:|--|
|Accounts|X|*Functionality supported in Advanced Trade API*|
|Addresses|✓|`restClient.AdvancedTradeApi.Account`|
|Transactions|✓|`restClient.AdvancedTradeApi.Account`|
|Deposits|✓|`restClient.AdvancedTradeApi.Account`|
|Withdrawals|✓|`restClient.AdvancedTradeApi.Account`|
|Currencies|✓|`restClient.AdvancedTradeApi.ExchangeData`|
|Exchange Rates|✓|`restClient.AdvancedTradeApi.ExchangeData`|
|Prices|✓|`restClient.AdvancedTradeApi.ExchangeData`|
|Time|✓|`restClient.AdvancedTradeApi.ExchangeData`|

## Support the project
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://advanced.coinbase.com/join/T6H54H8)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd 

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 3.5.0 - 10 Feb 2026
    * Updated CryptoExchange.Net to version 10.5.1, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added websocket symbol event topic mapping
    * Updated UserClientProvider internal client cache to non-static to prevent cleanup issues

* Version 3.4.0 - 06 Feb 2026
    * Updated CryptoExchange.Net to version 10.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added CoinbaseUserSpotDataTracker and CoinbaseUserFuturesDataTracker
    * Added additional methods for requesting supported symbols to Shared ISpotSymbolRestClient/IFuturesSymbolRestClient interfaces
    * Added PositionMode mapping on SharedPosition models
    * Added Status mapping for SharedDeposit models
    * Updated socket receive buffer size
    * Fixed unsubscription query not register sequence number
    * Fixed disposed clients getting returned from UserClientProvider
    * Fixed deserialization warning in futures symbols

* Version 3.3.1 - 27 Jan 2026
    * Updated CryptoExchange.Net to version 10.3.1, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes

* Version 3.3.0 - 22 Jan 2026
    * Updated CryptoExchange.Net to version 10.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Removed legacy websocket message handling and the corresponding UseUpdatedDeserialization client option
    * Added Metadata to AsterExchange

* Version 3.2.2 - 14 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.3, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes

* Version 3.2.1 - 13 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.2 to fix issue with socket message sequencing when having duplicate subscriptions

* Version 3.2.0 - 13 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.1, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added SequenceNumber to websocket updates
    * Added validation of sequence numbers for websocket connections
    * Fixed restClient.AdvancedTradeApi.Account.GetFeeInfoAsync deserialization, updated response model

* Version 3.1.0 - 07 Jan 2026
    * Updated CryptoExchange.Net version to 10.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added DataTimeLocal and DataAge properties to DataEvent object
    * Added UpdateServerTime, UpdateLocalTime and DataAge properties to (I)SymbolOrderBook
    * Added ExchangeApi to Rest client with some basic endpoints
    * Added check for max number of symbols to SubscribeToOrderBookUpdatesAsync stream

* Version 3.0.1 - 18 Dec 2025
    * Updated CryptoExchange.Net to 10.0.1 to fix parameter serialization error

* Version 3.0.0 - 16 Dec 2025
    * Added Net10.0 target framework
    * Updated CryptoExchange.Net version to 10.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Improved performance across the board, biggest gains in websocket message processing
    * Updated REST message response handling
    * Updated WebSocket message handling
    * Added UseUpdatedDeserialization socket client options to toggle by new and old message handlings
    * Added SocketIndividualSubscriptionCombineTarget socket client option
    * Updated Shared API's subscription update types from ExchangeEvent to DataEvent
    * Updated authentication to use Microsoft.IdentityModel.JsonWebTokens instead of jose-jwt

* Version 2.11.3 - 27 Nov 2025
    * Fixed AUDD-USDC symbol subscriptions in AdvancedTradeApi

* Version 2.11.2 - 19 Nov 2025
    * Fixed XSGD-USDC symbol subscriptions in AdvancedTradeApi

* Version 2.11.1 - 12 Nov 2025
    * Fixed socketClient.ExchangeApi subscriptions

* Version 2.11.0 - 11 Nov 2025
    * Updated CryptoExchange.Net version to 9.13.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added public streams for socketClient.ExchangeApi for the institutional websocket API

* Version 2.10.0 - 03 Nov 2025
    * Updated CryptoExchange.Net to version 9.12.0
    * Added support for using SharedSymbol.UsdOrStable in Shared APIs
    * Add AssetMigration type to TransactionType enum
    * Fixed exception when initial trade snapshot has no items in TradeTracker
    * Removed some unhelpful verbose logs

* Version 2.9.1 - 27 Oct 2025
    * Fixed quantity in Shared Rest UserTrade models not correctly handling quantities in quote asset

* Version 2.9.0 - 16 Oct 2025
    * Updated CryptoExchange.Net version to 9.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/

* Version 2.8.0 - 30 Sep 2025
    * Updated CryptoExchange.Net version to 9.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ITrackerFactory to TrackerFactory implementation
    * Updated CoinbaseTransaction response model

* Version 2.7.0 - 01 Sep 2025
    * Updated CryptoExchange.Net version to 9.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * HTTP REST requests will now use HTTP version 2.0 by default

* Version 2.6.0 - 25 Aug 2025
    * Updated CryptoExchange.Net version to 9.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClearUserClients method to user client provider

* Version 2.5.1 - 21 Aug 2025
    * Added check for parsing Unauthorized response

* Version 2.5.0 - 20 Aug 2025
    * Updated CryptoExchange.Net to version 9.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added improved error parsing
    * Fixed Shared GetBookTickerAsync endpoint not marked as requiring authentication
    * Fixed deserialization error CoinbaseOrderConfiguration

* Version 2.4.0 - 04 Aug 2025
    * Updated CryptoExchange.Net to version 9.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for multi-symbol Shared socket subscriptions

* Version 2.3.0 - 23 Jul 2025
    * Updated CryptoExchange.Net to version 9.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated websocket message matching

* Version 2.2.0 - 15 Jul 2025
    * Updated CryptoExchange.Net to version 9.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Fixed deserialization issue CoinbaseFuturesBalance

* Version 2.1.0 - 02 Jun 2025
    * Updated CryptoExchange.Net to version 9.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added (I)CoinbaseUserClientProvider allowing for easy client management when handling multiple users

* Version 2.0.0 - 13 May 2025
    * Updated CryptoExchange.Net to version 9.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to AdvandedTradeApi Shared clients
    * Added IBookTickerRestClient implementation to AdvandedTradeApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to AdvandedTradeApi Shared client
    * Added IFuturesTriggerOrderRestClient implementation to AdvandedTradeApi Shared client
    * Added IsTriggerOrder to SharedSpotOrder model
    * Added TriggerPrice, IsTriggerPrice to SharedFuturesOrder model
    * Added MaxLongLeverage, MaxShortLeverage to SharedFuturesSymbol model
    * Added QuoteVolume property mapping to SharedSpotTicker model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added error details parsing in restClient.AdvancedTradeApi.Trading.PlaceOrderAsync
    * Added All property to retrieve all available environment on CoinbaseEnvironment
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Removed Newtonsoft.Json dependency
    * Removed legacy AddCoinbase(restOptions, socketOptions) DI overload
    * Fixed duplicate symbols getting returned from the Shared GetSpotSymbolsAsync and GetSpotTickersAsync implementations
    * Fixed Shared GetBalancesAsync returning the same asset multiple times
    * Fixed incorrect DataTradeMode on certain Shared interface responses
    * Fixed some typos
    * Fixed deserialization error in restClient.AdvandedTradeApi.ExchangeData.GetFuturesSymbolsAsync endpoint

* Version 2.0.0-beta3 - 01 May 2025
    * Updated CryptoExchange.Net version to 9.0.0-beta5
    * Added property to retrieve all available API environments
    * Fixed duplicate symbols being returned from Shared GetSpotSymbolsAsync

* Version 2.0.0-beta2 - 23 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta2
    * Added Shared spot ticker QuoteVolume mapping
    * Fixed incorrect DataTradeMode on responses

* Version 2.0.0-beta1 - 22 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to AdvandedTradeApi Shared clients
    * Added IBookTickerRestClient implementation to AdvandedTradeApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to AdvandedTradeApi Shared client
    * Added IFuturesTriggerOrderRestClient implementation to AdvandedTradeApi Shared client
    * Added IsTriggerOrder to SharedSpotOrder model
    * Added TriggerPrice, IsTriggerPrice to SharedFuturesOrder model
    * Added MaxLongLeverage, MaxShortLeverage to SharedFuturesSymbol model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added error details parsing in restClient.AdvancedTradeApi.Trading.PlaceOrderAsync
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Removed Newtonsoft.Json dependency
    * Removed legacy AddCoinbase(restOptions, socketOptions) DI overload
    * Fixed some typos
    * Fixed deserialization error in restClient.AdvandedTradeApi.ExchangeData.GetFuturesSymbolsAsync endpoint

* Version 1.9.1 - 28 Mar 2025
    * Fixed deserialization issue for restClient.AdvancedTradeApi.Account.GetPerpetualPortfolioSummaryAsync

* Version 1.9.0 - 24 Mar 2025
    * Added attachedOrderTriggerPrice and attachedOrderLimitPrice parameters for restClient.AdvancedTradeApi.Trading.PlaceOrderAsync

* Version 1.8.1 - 12 Feb 2025
    * Fixed missing value PreLaunch for SymbolStatus enum

* Version 1.8.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Added getTradabilityStatus parameter to GetSymbolsAsync method
    * Renamed KlineInterval.TwoHour to KlineInterval.TwoHours, fixed int value
    * Fix Mono runtime exception on rest client construction using DI

* Version 1.7.2 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to CoinbaseExchange class

* Version 1.7.1 - 06 Jan 2025
    * Updated transaction model to include fee and quantity info

* Version 1.7.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client

* Version 1.6.1 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added Platform property to restClient.AdvancedTradeApi.Account.GetAccountsAsync and GetAccountAsync response model
    * Fixed orderbook creation via CoinbaseBookFactory

* Version 1.6.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated CoinbaseOptions to LibraryOptions implementation
    * Updated test and analyzer package versions

* Version 1.5.1 - 21 Nov 2024
    * Fixed deserialization error in SubscribeToBatchedTickerUpdatesAsync subscription when there is no trade price

* Version 1.5.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to CoinbaseExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant CoinbaseSocketClient constructor

* Version 1.4.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 1.3.0 - 04 Nov 2024
    * Updated restClient.AdvancedTradeApi.Account.WithdrawCryptoAsync parameters
    * Removed restClient.AdvancedTradeApi.Account.TransferAsync as it's no longer supported

* Version 1.2.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to CoinbaseExchange class
    * Added support Side setting on SharedTrade model
    * Added CoinbaseTrackerFactory for creating trackers
    * Added overload to Create method on CoinbaseOrderBookFactory support SharedSymbol parameter
    * Added GetKlinesAsync to Shared rest client
    * Fixed exception on restClient.AdvancedTradingAi.Trading.CancelOrderAynsc when order not found
    * Fixed exception on restClient.AdvancedTradingAi.Trading.CancelOrdersAynsc when request fails
    * Fixed restClient.AdvancedTradingAi.ExchangeData.GetKlinesAsync time filter
    * Fixed issue with concurrent websocket subscription acknowledgements
    * Removed incorrect rate limit of 100 message per second per ip for websockets

* Version 1.1.2 - 22 Oct 2024
    * Fixed deserialization issue on websocket ticker updates

* Version 1.1.1 - 21 Oct 2024
    * Fixed websocket market data subscriptions for "USDT-USDC" and "EURC-USDC" symbols

* Version 1.1.0 - 15 Oct 2024
    * Updated ExchangeData endpoints to use the Products endpoint instead of Public endpoint if API credentials are provided
    * Added restClient.AdvancedTradeApi.ExchangeData.GetBookTickersAsync and GetBookTickerAsync endpoints

* Version 1.0.1 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization
    * Fixed ICoinbaseOrderBookFactory DI lifetime

* Version 1.0.0 - 07 Oct 2024
    * Initial release
