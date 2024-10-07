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
        public static CoinbaseSocketOptions Default { get; set; } = new CoinbaseSocketOptions()
        {
            Environment = CoinbaseEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

         /// <summary>
        /// Advanced Trade API options
        /// </summary>
        public SocketApiOptions AdvancedTradeOptions { get; private set; } = new SocketApiOptions();

        internal CoinbaseSocketOptions Copy()
        {
            var options = Copy<CoinbaseSocketOptions>();
            options.AdvancedTradeOptions = AdvancedTradeOptions.Copy<SocketApiOptions>();
            return options;
        }
    }
}
