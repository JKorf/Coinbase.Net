using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Coinbase options
    /// </summary>
    public class CoinbaseOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public CoinbaseRestOptions Rest { get; set; } = new CoinbaseRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public CoinbaseSocketOptions Socket { get; set; } = new CoinbaseSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `CoinbaseEnvironment` to swap environment, for example `Environment = CoinbaseEnvironment.Live`
        /// </summary>
        public CoinbaseEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the ICoinbaseSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
