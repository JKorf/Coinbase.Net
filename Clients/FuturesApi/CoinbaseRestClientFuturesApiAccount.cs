using CryptoExchange.Net.Objects;
using Coinbase.Net.Clients.FuturesApi;
using Coinbase.Net.Interfaces.Clients.FuturesApi;

namespace Coinbase.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientFuturesApiAccount : ICoinbaseRestClientFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CoinbaseRestClientFuturesApi _baseClient;

        internal CoinbaseRestClientFuturesApiAccount(CoinbaseRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
