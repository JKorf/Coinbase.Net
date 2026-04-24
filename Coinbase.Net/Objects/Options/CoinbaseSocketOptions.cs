using CryptoExchange.Net.Authentication;
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
        /// The symbols for which USDC should not be replaced with USD when subscribing to streams for the symbol. Generally doesn't need to be changed, but can be used to add symbols that are added in the future that also shouldn't have USDC replaced with USD. 
        /// </summary>
        public string[] SymbolsUsdcReplaceFilter { get; set; } = new[]
        {
            "USDT-USDC",
            "EURC-USDC",
            "XSGD-USDC",
            "AUDD-USDC",
            "TGBP-USDC"
        };

        /// <summary>
        /// Advanced Trade API options
        /// </summary>
        public SocketApiOptions AdvancedTradeOptions { get; private set; } = new SocketApiOptions();


        internal CoinbaseSocketOptions Set(CoinbaseSocketOptions targetOptions)
        {
            targetOptions = base.Set<CoinbaseSocketOptions>(targetOptions);
            targetOptions.SymbolsUsdcReplaceFilter = SymbolsUsdcReplaceFilter;
            targetOptions.AdvancedTradeOptions = AdvancedTradeOptions.Set(targetOptions.AdvancedTradeOptions);
            return targetOptions;
        }
    }
}
