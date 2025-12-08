using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase API endpoints
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="ICoinbaseRestClientAdvancedTradeApiAccount"/>
        public ICoinbaseRestClientAdvancedTradeApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="ICoinbaseRestClientAdvancedTradeApiExchangeData"/>
        public ICoinbaseRestClientAdvancedTradeApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="ICoinbaseRestClientAdvancedTradeApiTrading"/>
        public ICoinbaseRestClientAdvancedTradeApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public ICoinbaseRestClientAdvancedTradeApiShared SharedClient { get; }
    }
}
