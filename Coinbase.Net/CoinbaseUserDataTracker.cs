using Coinbase.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace Coinbase.Net
{
    /// <inheritdoc />
    public class CoinbaseUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseUserSpotDataTracker(
            ILogger<CoinbaseUserSpotDataTracker> logger,
            ICoinbaseRestClient restClient,
            ICoinbaseSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.AdvancedTradeApi.SharedClient,
                null,
                restClient.AdvancedTradeApi.SharedClient,
                null,
                restClient.AdvancedTradeApi.SharedClient,
                socketClient.AdvancedTradeApi.SharedClient,
                null,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc />
    public class CoinbaseUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc />
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseUserFuturesDataTracker(
            ILogger<CoinbaseUserFuturesDataTracker> logger,
            ICoinbaseRestClient restClient,
            ICoinbaseSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.AdvancedTradeApi.SharedClient,
                null,
                restClient.AdvancedTradeApi.SharedClient,
                null,
                restClient.AdvancedTradeApi.SharedClient,
                socketClient.AdvancedTradeApi.SharedClient,
                null,
                socketClient.AdvancedTradeApi.SharedClient,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }
}
