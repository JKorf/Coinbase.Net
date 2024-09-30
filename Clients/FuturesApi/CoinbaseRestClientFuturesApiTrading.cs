using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Interfaces.Clients.FuturesApi;

namespace Coinbase.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientFuturesApiTrading : ICoinbaseRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CoinbaseRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal CoinbaseRestClientFuturesApiTrading(ILogger logger, CoinbaseRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }
    }
}
