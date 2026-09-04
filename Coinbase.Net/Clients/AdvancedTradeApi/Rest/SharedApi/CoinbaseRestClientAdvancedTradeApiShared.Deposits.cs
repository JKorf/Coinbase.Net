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

        #region Get Deposit Addresses

        async Task<ICallResult<SharedDepositAddress[]>> IGetDepositAddresses.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
            => await GetDepositAddressesAsync(request, ct).ConfigureAwait(false);

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var depositAddresses = await _api.Account.GetDepositAddressesAsync(accountId, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Data.Select(x => new SharedDepositAddress(x.Network, x.Address)
            {
                Network = x.Network
            }).ToArray());
        }

        #endregion

        #region Get Deposit History

        async Task<ICallResult<SharedDeposit[]>> IGetDepositHistory.GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetDepositHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetDepositHistoryAsync(request, pageRequest, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;


        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, true, true, false, 100)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetDepositsAsync(
                accountId,
                order: direction == DataDirection.Descending ? SortOrder.Descending : SortOrder.Ascending,
                fromId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toId: direction == DataDirection.Descending ? pageParams.FromId : null,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => direction == DataDirection.Ascending 
                    ? Pagination.NextPageFromId(result.Data.Data.OrderByDescending(x => x.CreateTime).First().Id)
                    : Pagination.NextPageFromId(result.Data.Data.OrderBy(x => x.CreateTime).First().Id),
                result.Data.Data.Length,
                result.Data.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x =>  
                            new SharedDeposit(
                                x.Quantity.Asset,
                                x.Quantity.Value,
                                x.Status == Enums.WithdrawalStatus.Completed,
                                x.CreateTime,
                                ParseTransferStatus(x.Status))
                            {
                                Id = x.Id
                            })
                       .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus ParseTransferStatus(WithdrawalStatus status)
        {
            if (status == WithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;
            if (status == WithdrawalStatus.Canceled)
                return SharedTransferStatus.Failed;
            if (status == WithdrawalStatus.Created)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

    }
}
