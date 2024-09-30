using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Interfaces.Clients.FuturesApi;
using Coinbase.Net.Objects.Models;

namespace Coinbase.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientFuturesApiExchangeData : ICoinbaseRestClientFuturesApiExchangeData
    {
        private readonly CoinbaseRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal CoinbaseRestClientFuturesApiExchangeData(ILogger logger, CoinbaseRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "XXX", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseModel>(request, null, ct).ConfigureAwait(false);
            throw new NotImplementedException();
        }

        #endregion
    }
}
