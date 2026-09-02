using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase API endpoints
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApi : IRestApiClient<CoinbaseCredentials>, IDisposable
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
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public ICoinbaseRestClientAdvancedTradeApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public ICoinbaseRestClientAdvancedTradeSharedApi SharedApi { get; }
    }
}
