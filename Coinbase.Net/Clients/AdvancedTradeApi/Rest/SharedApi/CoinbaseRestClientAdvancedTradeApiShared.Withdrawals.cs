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

        #region Get Withdrawal History

        async Task<ICallResult<SharedWithdrawal[]>> IGetWithdrawalHistory.GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetWithdrawalHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, true, true, false, 100)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Account.GetWithdrawalsAsync(
                accountId,
                order: direction == DataDirection.Descending ? SortOrder.Descending : SortOrder.Ascending,
                fromId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toId: direction == DataDirection.Descending ? pageParams.FromId : null,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

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
                            new SharedWithdrawal(
                                x.Quantity.Asset, 
                                string.Empty,
                                x.Quantity.Value, 
                                x.Status == Enums.WithdrawalStatus.Completed, 
                                x.CreateTime,
                                GetWithdrawalStatus(x))
                            {
                                Id = x.Id,
                                Fee = x.Fee.Value
                            })
                       .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus GetWithdrawalStatus(CoinbaseWithdrawal x)
        {
            if (x.Status == WithdrawalStatus.Canceled)
                return SharedTransferStatus.Failed;

            if (x.Status == WithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawalStatus.Created)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }



        #region Withdraw

        async Task<ICallResult<SharedId>> IWithdraw.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
            => await WithdrawAsync(request, ct).ConfigureAwait(false);

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to withdraw from", "123123")
            }
        };
        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            // Get data
            var withdrawal = await _api.Account.WithdrawCryptoAsync(
                accountId,
                request.Address,
                request.Quantity,
                request.Asset,
                destinationTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.Id));
        }

        #endregion

    }
}
