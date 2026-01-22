using Coinbase.Net.Clients;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coinbase.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateConcurrentSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new CoinbaseSocketClient(Options.Create(new CoinbaseSocketOptions
            {
                OutputOriginalData = true
            }), logger);

            var tester = new SocketSubscriptionValidator<CoinbaseSocketClient>(client, "Subscriptions/AdvancedTrade", "wss://advanced-trade-ws.coinbase.com", "events.0");
            await tester.ValidateConcurrentAsync<CoinbaseStreamKline[]>(
                (client, handler) => client.AdvancedTradeApi.SubscribeToKlineUpdatesAsync("ETH-USDT", handler),
                (client, handler) => client.AdvancedTradeApi.SubscribeToKlineUpdatesAsync("BTC-USDT", handler),
                "Concurrent");
        }

#warning add ExchangeApi
        [Test]
        public async Task ValidateAdvancedTradeExchangeDataSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new CoinbaseSocketClient(Options.Create(new CoinbaseSocketOptions
            {
                ApiCredentials = new ApiCredentials("123", "-----BEGIN EC PRIVATE KEY-----\r\nMHcCAQEEIGaopmcUKDBihelMJbKUyRmaR6F3Eo90EZaqZJ3/mBr0oAoGCCqGSM49\r\nAwEHoUQDQgAEnYaxPG+o57xM5o/M5QNn0ocwlw12ZNVWFEo9tKDQ7Jz5Gz/0eMcP\r\nmEhm5msFFpWgrY0/T92MfwByuaLws/rM3w==\r\n-----END EC PRIVATE KEY-----"),
                OutputOriginalData = true
            }), logger);
            var tester = new SocketSubscriptionValidator<CoinbaseSocketClient>(client, "Subscriptions/AdvancedTrade", "wss://advanced-trade-ws.coinbase.com", "events.0");
            await tester.ValidateAsync<CoinbaseHeartbeat>((client, handler) => client.AdvancedTradeApi.SubscribeToHeartbeatUpdatesAsync(handler), "Heartbeat");
            await tester.ValidateAsync<CoinbaseTrade[]>((client, handler) => client.AdvancedTradeApi.SubscribeToTradeUpdatesAsync("ETH-USDT", handler), "Trades", "events.0.trades");
            await tester.ValidateAsync<CoinbaseStreamKline[]>((client, handler) => client.AdvancedTradeApi.SubscribeToKlineUpdatesAsync("ETH-USDT", handler), "Klines", "events.0.candles");
            await tester.ValidateAsync<CoinbaseTicker>((client, handler) => client.AdvancedTradeApi.SubscribeToTickerUpdatesAsync("ETH-USDT", handler), "Ticker", "events.0.tickers.0");
            await tester.ValidateAsync<CoinbaseBatchTicker>((client, handler) => client.AdvancedTradeApi.SubscribeToBatchedTickerUpdatesAsync("ETH-USDT", handler), "BatchTicker", "events.0.tickers.0");
            await tester.ValidateAsync<CoinbaseStreamSymbol>((client, handler) => client.AdvancedTradeApi.SubscribeToSymbolUpdatesAsync("ETH-USDT", handler), "Symbol", "events.0.products.0");
            await tester.ValidateAsync<CoinbaseUserUpdate>((client, handler) => client.AdvancedTradeApi.SubscribeToUserUpdatesAsync(handler), "User", "events.0", ignoreProperties: ["end_time", "start_time"]);
            await tester.ValidateAsync<CoinbaseFuturesBalance>((client, handler) => client.AdvancedTradeApi.SubscribeToFuturesBalanceUpdatesAsync(handler), "FuturesBalance", "events.0.fcm_balance_summary");
        }
    }
}
