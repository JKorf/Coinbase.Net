using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Coinbase.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Coinbase Rest API. 
    /// </summary>
    public interface ICoinbaseRestClient : IRestClient<CoinbaseCredentials>
    {
        /// <summary>
        /// Advanced Trade API endpoints, also contains some App API endpoints
        /// </summary>
        /// <see cref="ICoinbaseRestClientAdvancedTradeApi"/>
        public ICoinbaseRestClientAdvancedTradeApi AdvancedTradeApi { get; }

        /// <summary>
        /// Exchange API endpoints, includes some more detailed currency and market data endpoints
        /// </summary>
        /// <see cref="ICoinbaseRestClientExchangeApi"/>
        public ICoinbaseRestClientExchangeApi ExchangeApi { get; }
    }
}
