using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Coinbase websocket API
    /// </summary>
    public interface ICoinbaseSocketClient : ISocketClient
    {
        /// <summary>
        /// Advanced trade API streams (Consumer API)
        /// </summary>
        /// <see cref="ICoinbaseSocketClientAdvancedTradeApi"/>
        public ICoinbaseSocketClientAdvancedTradeApi AdvancedTradeApi { get; }
        /// <summary>
        /// Exchange API streams (Institutional API)
        /// </summary>
        public ICoinbaseSocketClientExchangeApi ExchangeApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
