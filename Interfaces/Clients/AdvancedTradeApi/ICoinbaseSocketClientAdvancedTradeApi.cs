using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using Coinbase.Net.Objects.Models;
using System.Collections.Generic;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase streams
    /// </summary>
    public interface ICoinbaseSocketClientAdvancedTradeApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to the heartbeats channel. The heartbeats channel can be used to keep the connection alive when data from other subscriptions isn't continuous
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#heartbeats-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(Action<DataEvent<CoinbaseHeartbeat>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trades updates
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#market-trades-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CoinbaseTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trades updates
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#market-trades-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<CoinbaseTrade>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates. Klines are always at a 5 minute interval.
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#candles-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamKline>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates. Klines are always at a 5 minute interval.
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#candles-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamKline>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates, updates are pushed immediately on any chance
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to ticker updates, updates are pushed immediately on any chance
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbol, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to batched ticker updates, updates are pushed every 5 seconds
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-batch-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to batched ticker updates, updates are pushed every 5 seconds
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-batch-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol rules updates
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#status-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol rules updates
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#status-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol rules updates
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#status-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string>? symbols, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates. First update is a snapshot of the full book, subsequent updates are change messages
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#level2-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to order book updates. First update is a snapshot of the full book, subsequent updates are change messages
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#level2-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order and position updates. After subscribing snapshots will be pushed containing all open orders for the user
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#user-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserUpdatesAsync(Action<DataEvent<CoinbaseUserUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to futures balance updates
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#futures-balance-summary-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFuturesBalanceUpdatesAsync(Action<DataEvent<CoinbaseFuturesBalance>> onMessage, CancellationToken ct = default);
    }
}
