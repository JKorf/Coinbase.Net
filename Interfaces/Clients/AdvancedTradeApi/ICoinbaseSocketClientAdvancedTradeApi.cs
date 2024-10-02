using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using Coinbase.Net.Objects.Models;
using System.Collections.Generic;

namespace Coinbase.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Coinbase streams
    /// </summary>
    public interface ICoinbaseSocketClientAdvancedTradeApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(Action<DataEvent<CoinbaseHeartbeat>> onMessage, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CoinbaseTrade>>> onMessage, CancellationToken ct = default);
    }
}
