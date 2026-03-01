using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Coinbase.Net.Objects.Models;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces.Clients;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase streams
    /// </summary>
    public interface ICoinbaseSocketClientAdvancedTradeApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        ICoinbaseSocketClientAdvancedTradeApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to the heartbeats channel. The heartbeats channel can be used to keep the connection alive when data from other subscriptions isn't continuous. When using this subscription it is recommended to also set the client option <code>options.SocketNoData = TimeSpan.FromSeconds(5)</code> to quickly detect when the connection is interupted.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#heartbeats-channel" /><br />
        /// Endpoint:<br />
        /// WS heartbeats
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(Action<DataEvent<CoinbaseHeartbeat>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trades updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#market-trades-channel" /><br />
        /// Endpoint:<br />
        /// WS market_trades
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trades updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#market-trades-channel" /><br />
        /// Endpoint:<br />
        /// WS market_trades
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseTrade[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates. Klines are always at a 5 minute interval.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#candles-channel" /><br />
        /// Endpoint:<br />
        /// WS candles
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamKline[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline updates. Klines are always at a 5 minute interval.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#candles-channel" /><br />
        /// Endpoint:<br />
        /// WS candles
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamKline[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates, updates are pushed immediately on any chance
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-channel" /><br />
        /// Endpoint:<br />
        /// WS ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to ticker updates, updates are pushed immediately on any chance
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-channel" /><br />
        /// Endpoint:<br />
        /// WS ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbol, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to batched ticker updates, updates are pushed every 5 seconds
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-batch-channel" /><br />
        /// Endpoint:<br />
        /// WS ticker_batch
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to batched ticker updates, updates are pushed every 5 seconds
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#ticker-batch-channel" /><br />
        /// Endpoint:<br />
        /// WS ticker_batch
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol rules updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#status-channel" /><br />
        /// Endpoint:<br />
        /// WS status
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol rules updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#status-channel" /><br />
        /// Endpoint:<br />
        /// WS status
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates. First update is a snapshot of the full book, subsequent updates are change messages
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#level2-channel" /><br />
        /// Endpoint:<br />
        /// WS level2
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to order book updates. First update is a snapshot of the full book, subsequent updates are change messages
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#level2-channel" /><br />
        /// Endpoint:<br />
        /// WS level2
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order and position updates. After subscribing snapshots will be pushed containing all open orders for the user
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#user-channel" /><br />
        /// Endpoint:<br />
        /// WS user
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserUpdatesAsync(Action<DataEvent<CoinbaseUserUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to futures balance updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/docs/ws-channels#futures-balance-summary-channel" /><br />
        /// Endpoint:<br />
        /// WS futures_balance_summary
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFuturesBalanceUpdatesAsync(Action<DataEvent<CoinbaseFuturesBalance>> onMessage, CancellationToken ct = default);
    }
}
