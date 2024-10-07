using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Options for the CoinbaseRestClient
    /// </summary>
    public class CoinbaseRestOptions : RestExchangeOptions<CoinbaseEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static CoinbaseRestOptions Default { get; set; } = new CoinbaseRestOptions()
        {
            Environment = CoinbaseEnvironment.Live,
            AutoTimestamp = true
        };

         /// <summary>
        /// Advanced Trade API options
        /// </summary>
        public RestApiOptions AdvancedTradeOptions { get; private set; } = new RestApiOptions();


        internal CoinbaseRestOptions Copy()
        {
            var options = Copy<CoinbaseRestOptions>();
            options.AdvancedTradeOptions = AdvancedTradeOptions.Copy<RestApiOptions>();
            return options;
        }
    }
}
