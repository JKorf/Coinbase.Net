using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Options for the CoinbaseSocketClient
    /// </summary>
    public class CoinbaseSocketOptions : SocketExchangeOptions<CoinbaseEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static CoinbaseSocketOptions Default { get; set; } = new CoinbaseSocketOptions()
        {
            Environment = CoinbaseEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Advanced Trade API options
        /// </summary>
        public SocketApiOptions AdvancedTradeOptions { get; private set; } = new SocketApiOptions();


        internal CoinbaseSocketOptions Set(CoinbaseSocketOptions targetOptions)
        {
            targetOptions = base.Set<CoinbaseSocketOptions>(targetOptions);
            targetOptions.AdvancedTradeOptions = AdvancedTradeOptions.Set(targetOptions.AdvancedTradeOptions);
            return targetOptions;
        }
    }
}
