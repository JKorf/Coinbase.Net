using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Interfaces.Clients.SpotApi;

namespace Coinbase.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientSpotApiTrading : ICoinbaseRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CoinbaseRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal CoinbaseRestClientSpotApiTrading(ILogger logger, CoinbaseRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }
    }
}
