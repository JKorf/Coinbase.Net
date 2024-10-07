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
        /// Futures API options
        /// </summary>
        public RestApiOptions FuturesOptions { get; private set; } = new RestApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();


        internal CoinbaseRestOptions Copy()
        {
            var options = Copy<CoinbaseRestOptions>();
            
            options.FuturesOptions = FuturesOptions.Copy<RestApiOptions>();

            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();

            return options;
        }
    }
}
