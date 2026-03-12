using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Options for the CoinbaseSocketClient
    /// </summary>
    public class CoinbaseSocketOptions : SocketExchangeOptions<CoinbaseEnvironment, CoinbaseCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static CoinbaseSocketOptions Default { get; set; } = new CoinbaseSocketOptions()
        {
            Environment = CoinbaseEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            ReceiveBufferSize = 524288 // Increased buffer so snapshot data fits better
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
        public SocketApiOptions<CoinbaseCredentials> AdvancedTradeOptions { get; private set; } = new SocketApiOptions<CoinbaseCredentials>();


        internal CoinbaseSocketOptions Set(CoinbaseSocketOptions targetOptions)
        {
            targetOptions = base.Set<CoinbaseSocketOptions>(targetOptions);
            targetOptions.AdvancedTradeOptions = AdvancedTradeOptions.Set(targetOptions.AdvancedTradeOptions);
            return targetOptions;
        }
    }
}
