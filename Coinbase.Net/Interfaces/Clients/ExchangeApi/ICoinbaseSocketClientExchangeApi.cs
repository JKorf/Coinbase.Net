using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using Coinbase.Net.Objects.Models;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces.Clients;

namespace Coinbase.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Coinbase Exchange API streams
    /// </summary>
    public interface ICoinbaseSocketClientExchangeApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to symbol heartbeat updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#heartbeat-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExHeartbeat>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to symbol heartbeat updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#heartbeat-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExHeartbeat>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol and asset status updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#status-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToExchangeInfoUpdatesAsync(Action<DataEvent<CoinbaseExchangeInfo>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol price ticker updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#ticker-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol price ticker updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#ticker-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol price ticker updates, batched to be pushed every 5 seconds if there is a change
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#ticker-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to symbol price ticker updates, batched to be pushed every 5 seconds if there is a change
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#ticker-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#level2-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onSnapshot">The event handler for when a full snapshot is received</param>
        /// <param name="onUpdate">The event handler for a change is received</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExBookSnapshot>> onSnapshot, Action<DataEvent<CoinbaseExBookUpdate>> onUpdate, CancellationToken ct = default);


        /// <summary>
        /// Subscribe to order book updates
        /// <para><a href="https://docs.cdp.coinbase.com/exchange/websocket-feed/channels#level2-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onSnapshot">The event handler for when a full snapshot is received</param>
        /// <param name="onUpdate">The event handler for a change is received</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExBookSnapshot>> onSnapshot, Action<DataEvent<CoinbaseExBookUpdate>> onUpdate, CancellationToken ct = default);

    }
}
