using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
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
        /// Advanced trade API streams
        /// </summary>
        public ICoinbaseSocketClientAdvancedTradeApi AdvancedTradeApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
