using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Interfaces.Clients.SpotApi;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Objects.Options;
using Coinbase.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using Coinbase.Net.Objects.Internal;
using System.Collections.Generic;

namespace Coinbase.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the Coinbase websocket Api
    /// </summary>
    internal partial class CoinbaseSocketClientAdvancedTradeApi : SocketApiClient, ICoinbaseSocketClientAdvancedTradeApi
    {
        #region fields
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal CoinbaseSocketClientAdvancedTradeApi(ILogger logger, CoinbaseSocketOptions options) :
            base(logger, options.Environment.SocketClientPublicAddress!, options, options.SpotOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(Action<DataEvent<CoinbaseHeartbeat>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseHeartbeat>(_logger, "heartbeats", new [] { "XXX" }, x => onMessage(x.As(x.Data.First())), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<CoinbaseTrade>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseTradeEvent>(_logger, "market_trades", new[] { symbol }, x => onMessage(x.As(x.Data.First().Trades)), false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
#warning symbol
            return message.GetValue<string>(_channelPath);
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection) => null;

        /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradeMode, DateTime? deliverDate) => throw new NotImplementedException();
    }
}
