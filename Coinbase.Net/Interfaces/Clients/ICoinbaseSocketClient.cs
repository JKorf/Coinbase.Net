using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

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
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
