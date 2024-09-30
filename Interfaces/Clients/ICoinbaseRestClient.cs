using Coinbase.Net.Interfaces.Clients.FuturesApi;
using Coinbase.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;

namespace Coinbase.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Coinbase Rest API. 
    /// </summary>
    public interface ICoinbaseRestClient : IRestClient
    {
        
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        public ICoinbaseRestClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        public ICoinbaseRestClientSpotApi SpotApi { get; }


        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
