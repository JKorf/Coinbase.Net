using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;

namespace Coinbase.Net
{
    /// <summary>
    /// Coinbase exchange information and configuration
    /// </summary>
    public static class CoinbaseExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Coinbase";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.coinbase.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://docs.cdp.coinbase.com/advanced-trade/reference",
            "https://docs.cdp.coinbase.com/coinbase-app/docs/welcome"
            };

        /// <summary>
        /// Rate limiter configuration for the Coinbase API
        /// </summary>
        public static CoinbaseRateLimiters RateLimiter { get; } = new CoinbaseRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Coinbase API
    /// </summary>
    public class CoinbaseRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal CoinbaseRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            CoinbaseRestPublic = new RateLimitGate("Coinbase Public")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, Array.Empty<IGuardFilter>(), 10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            CoinbaseRestPrivate = new RateLimitGate("Coinbase Private")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKey, Array.Empty<IGuardFilter>(), 30, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            CoinbaseSocket = new RateLimitGate("Coinbase Socket")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new LimitItemTypeFilter(RateLimitItemType.Request), 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding))
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new LimitItemTypeFilter(RateLimitItemType.Request), 8, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            CoinbaseRestPublic.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            CoinbaseRestPrivate.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            CoinbaseSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate CoinbaseRestPublic { get; private set; }
        internal IRateLimitGate CoinbaseRestPrivate { get; private set; }
        internal IRateLimitGate CoinbaseSocket { get; private set; }

    }
}
