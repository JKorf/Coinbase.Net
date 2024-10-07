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
        /// Futures API options
        /// </summary>
        public SocketApiOptions FuturesOptions { get; private set; } = new SocketApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();


        internal CoinbaseSocketOptions Copy()
        {
            var options = Copy<CoinbaseSocketOptions>();
            
            options.FuturesOptions = FuturesOptions.Copy<SocketApiOptions>();

            options.SpotOptions = SpotOptions.Copy<SocketApiOptions>();

            return options;
        }
    }
}
