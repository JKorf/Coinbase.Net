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
        internal static CoinbaseRestOptions Default { get; set; } = new CoinbaseRestOptions()
        {
            Environment = CoinbaseEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Advanced Trade API options
        /// </summary>
        public RestApiOptions AdvancedTradeOptions { get; private set; } = new RestApiOptions();

        internal CoinbaseRestOptions Set(CoinbaseRestOptions targetOptions)
        {
            targetOptions = base.Set<CoinbaseRestOptions>(targetOptions);
            targetOptions.AdvancedTradeOptions = AdvancedTradeOptions.Set(targetOptions.AdvancedTradeOptions);
            return targetOptions;
        }
    }
}
