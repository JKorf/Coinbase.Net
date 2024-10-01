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

namespace Coinbase.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the Coinbase websocket Api
    /// </summary>
    internal partial class CoinbaseSocketClientAdvancedTradeApi : SocketApiClient, ICoinbaseSocketClientAdvancedTradeApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
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
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<CoinbaseModel>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseModel>(_logger, new [] { "XXX" }, onMessage, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            return message.GetValue<string>(_idPath);
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection) => null;

        /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradeMode, DateTime? deliverDate) => throw new NotImplementedException();
    }
}
