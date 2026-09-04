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

        public SharedFeeDeductionType FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        public SharedFeeAssetType FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;

        public SharedOrderType[] FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        public SharedTimeInForce[] FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        #region Place Futures Order

        async Task<ICallResult<SharedId>> IPlaceFuturesOrder.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
            => await PlaceFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesOrderOptions PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false);
        public async Task<HttpResult<SharedId>> PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (request.ReduceOnly == true)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.ReduceOnly), $"ReduceOnly flag is not available on {Exchange}, use ClosePositionAsync with quantity to reduce a position"));

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInContracts,
                price: request.Price,
                leverage: request.Leverage,
                marginType: request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Cross ? MarginType.Cross : MarginType.Isolated,
                // null, not false, for anything that is not post-only: post_only is not a field of the
                // market_market_ioc configuration, and Coinbase rejects the whole order with
                // 'proto: unknown field "post_only"' when it is sent on a futures market order
                // (observed 2026-08-03). Same shape the spot path already uses.
                postOnly: request.OrderType == SharedOrderType.LimitMaker ? true : null,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId.ToString()));
        }

        #endregion

        #region Get Futures Order

        async Task<ICallResult<SharedFuturesOrder>> IGetFuturesOrder.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderOptions GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder>> GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : order.Data.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderStatus),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.OrderConfiguration.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.QuoteQuantityFilled, contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                Leverage = order.Data.Leverage,
                TriggerPrice = order.Data.OrderConfiguration.StopPrice,
                IsTriggerOrder = order.Data.OrderType == OrderType.Stop || order.Data.OrderType == OrderType.StopLimit
            });
        }

        #endregion

        #region Get Open Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetOpenFuturesOrders.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
            => await GetOpenFuturesOrdersAsync(request, ct).ConfigureAwait(false);

        public GetOpenFuturesOrdersOptions GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var expiryType = ((request.Symbol?.TradingMode ?? request.TradingMode) ?? TradingMode.PerpetualLinear) == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var orders = await _api.Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Open],
                symbolType: SymbolType.Futures,
                expiryType: expiryType,
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderStatus),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice == 0 ? null : x.AverageFillPrice,
                OrderPrice = x.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.OrderConfiguration.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.QuoteQuantityFilled, contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.LastFillTime,
                Leverage = x.Leverage,
                TriggerPrice = x.OrderConfiguration.StopPrice,
                IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
            }).ToArray());
        }

        #endregion

        #region Get Closed Futures Orders

        async Task<ICallResult<SharedFuturesOrder[]>> IGetClosedFuturesOrders.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetClosedFuturesOrdersAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFuturesClosedOrdersOptions GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedFuturesOrder[]>> GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var expiryType = request.Symbol!.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var result = await _api.Trading.GetOrdersAsync(
                symbols: [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Canceled, OrderStatus.Filled],
                symbolType: SymbolType.Futures,
                expiryType: expiryType,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                result.Data.Length,
                result.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedFuturesOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                                x.Symbol,
                                x.OrderId.ToString(),
                                x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                ParseOrderStatus(x.OrderStatus),
                                x.CreateTime)
                            {
                                ClientOrderId = x.ClientOrderId,
                                AveragePrice = x.AverageFillPrice == 0 ? null : x.AverageFillPrice,
                                OrderPrice = x.OrderConfiguration.Price,
                                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.OrderConfiguration.Quantity),
                                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.QuoteQuantityFilled, contractQuantity: x.QuantityFilled),
                                TimeInForce = ParseTimeInForce(x.TimeInForce),
                                UpdateTime = x.LastFillTime,
                                Leverage = x.Leverage,
                                TriggerPrice = x.OrderConfiguration.StopPrice,
                                IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
                            }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Get Futures Order Trades

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesOrderTrades.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
            => await GetFuturesOrderTradesAsync(request, ct).ConfigureAwait(false);

        public GetFuturesOrderTradesOptions GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var orders = await _api.Trading.GetUserTradesAsync(orderIds: [request.OrderId], ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                new SharedOrderQuantity(x.Quantity),
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        #endregion
        
        #region Get Futures User Trade History

        async Task<ICallResult<SharedUserTrade[]>> IGetFuturesUserTradeHistory.GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFuturesUserTradeHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetFuturesUserTradeHistoryAsync(request, pageRequest, ct);
        GetFuturesUserTradeHistoryOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions => GetFuturesUserTradeHistoryOptions;

        public GetFuturesUserTradeHistoryOptions GetFuturesUserTradeHistoryOptions { get; } = new GetFuturesUserTradeHistoryOptions(_exchangeName, false, true, true, 100);
        public async Task<HttpResult<SharedUserTrade[]>> GetFuturesUserTradeHistoryAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFuturesUserTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await _api.Trading.GetUserTradesAsync(
                symbols: [request.Symbol!.GetSymbol(FormatSymbol)],
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                cursor: pageParams.Cursor,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => result.Data.Cursor == null ? null : Pagination.NextPageFromCursor(result.Data.Cursor),
                result.Data.Trades.Length,
                result.Data.Trades.Select(x => x.Timestamp),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                                x.Symbol,
                                x.OrderId,
                                x.TradeId,
                                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                new SharedOrderQuantity(x.Quantity),
                                x.Price,
                                x.Timestamp)
                            {
                                Fee = x.Fee,
                                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
                            })
                       .ToArray(), nextPageRequest);
        }

        #endregion

        #region Cancel Futures Order

        async Task<ICallResult<SharedId>> ICancelFuturesOrder.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesOrderOptions CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId!));
        }

        #endregion

        #region Get Positions

        async Task<ICallResult<SharedPosition[]>> IGetPositions.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
            => await GetPositionsAsync(request, ct).ConfigureAwait(false);

        public GetPositionsOptions GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        public async Task<HttpResult<SharedPosition[]>> GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var tradingMode = request.Symbol?.TradingMode ?? request.TradingMode;
            if (tradingMode == null || request.TradingMode == TradingMode.PerpetualLinear)
            {
                var portfolioId = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "PortfolioId");
                if (portfolioId == default)
                    return HttpResult.Fail<SharedPosition[]>(Exchange, ArgumentError.Missing("PortfolioId", "PortfolioId is required as Exchange parameter for retrieving Perpetual futures balances"));

                var result = await _api.Trading.GetPerpetualPositionsAsync(portfolioId, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, result.Data.Positions.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        new SharedOrderQuantity(Math.Abs(x.NetQuantity)),
                        null)
                    {
                        UnrealizedPnl = x.UnrealizedPnl.Value,
                        LiquidationPrice = x.LiquidationPrice.Value == 0 ? null : x.LiquidationPrice.Value,
                        Leverage = x.Leverage,
                        AverageOpenPrice = x.EntryVolumeWeightedAveragePrice.Value,
                        PositionMode = SharedPositionMode.HedgeMode,
                        PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                    }).ToArray());
            }
            else
            {
                var result = await _api.Trading.GetFuturesPositionsAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => 
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol), 
                        x.Symbol,
                        new SharedOrderQuantity(Math.Abs(x.NumberOfContracts)),
                        null)
                    {
                        UnrealizedPnl = x.UnrealizedPnl,
                        AverageOpenPrice = x.AverageEntryPrice,
                        PositionMode = SharedPositionMode.HedgeMode,
                        PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                    }).ToArray());
            }
        }

        #endregion

        #region Close Position

        async Task<ICallResult<SharedId>> IClosePosition.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
            => await ClosePositionAsync(request, ct).ConfigureAwait(false);

        public ClosePositionOptions ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await _api.Trading.ClosePositionAsync(
                symbol,
                quantity: request.Quantity,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        #endregion

    }
}
