using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net;
using Coinbase.Net.Enums;
using CryptoExchange.Net.Objects.Errors;
using Coinbase.Net.Objects.Models;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    internal partial class CoinbaseRestClientAdvancedTradeSharedApi
    {
        private async Task<string?> GetAccountIdAsync(ExchangeParameters? parameters, string? asset, CancellationToken ct)
        {
            var accountId = ExchangeParameters.GetValue<string>(parameters, Exchange, "AccountId");
            if (accountId != default)
                return accountId;

            if (asset == null)
                return null;

            var accounts = await _api.Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
            if (!accounts.Success)
                return null;

            var account = accounts.Data.Accounts.FirstOrDefault(x => x.Asset == asset);
            if (account == null)
                return null;

            return account.AccountId;
        }
    }
}
