# ![Coinbase.Net](https://raw.githubusercontent.com/JKorf/Coinbase.Net/master/Coinbase.Net/Icon/icon.png) Coinbase.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Coinbase.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Coinbase.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Coinbase.Net?style=for-the-badge)

Coinbase.Net is a client library for accessing the [Coinbase Advanced Trade REST and Websocket API](https://docs.cdp.coinbase.com/advanced-trade/docs/welcome) and [Coinbase App REST API](https://docs.cdp.coinbase.com/coinbase-app/docs/welcome). 

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as dotnet 8.0 and 9.0 to use the latest framework features.

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

For information on the clients, dependency injection, response processing and more see the [documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples [here](https://github.com/JKorf/Coinbase.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Coinbase.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net/#idocs_shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
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
