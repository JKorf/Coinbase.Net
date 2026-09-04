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
        #region Get Recent Trades

        async Task<ICallResult<SharedTrade[]>> IGetRecentTrades.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
            => await GetRecentTradesAsync(request, ct).ConfigureAwait(false);

        public GetRecentTradesOptions GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 1000, false);

        public async Task<HttpResult<SharedTrade[]>> GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTradeHistoryAsync(
                symbol,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            // Return
            return HttpResult.Ok(result, result.Data.Trades.Select(x => 
            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(x.Quantity), x.Price, x.Timestamp)
            {
                Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }

        #endregion

        #region Get Trade History

        async Task<ICallResult<SharedTrade[]>> IGetTradeHistory.GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetTradeHistoryOptions GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(_exchangeName, false, true, true, 1000, false);

        public async Task<HttpResult<SharedTrade[]>> GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.ExchangeData.GetTradeHistoryAsync(
                symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                // Filtering is done on second level, so need to adjust to previous second instead of millisecond
                () => Pagination.NextPageFromTime(pageParams, result.Data.Trades.Min(x => x.Timestamp).Add(TimeSpan.FromMilliseconds(-999))),
                result.Data.Trades.Length,
                result.Data.Trades.Select(x => x.Timestamp),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                       .Select(x =>  
                            new SharedTrade(request.Symbol, symbol, new SharedOrderQuantity(x.Quantity), x.Price, x.Timestamp)
                            {
                                Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                            })
                       .ToArray(), nextPageRequest);
        }

        #endregion
    }
}
