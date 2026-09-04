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
        #region Place Futures Trigger Order

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, true)
        {
        };

        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side,
                NewOrderType.StopLimit,
                quantity: request.Quantity.QuantityInContracts,
                // Simulate market order by adding/removing 10% to the trigger price as order price
                price: request.OrderPrice ?? (request.TriggerPrice + (request.TriggerPrice * 0.1m * (request.OrderDirection == SharedTriggerOrderDirection.Enter ? 1 : -1))),
                stopPrice: request.TriggerPrice,
                stopDirection: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopDirection.Up : StopDirection.Down,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        #endregion

        private OrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
            {
                if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
                    return OrderSide.Buy;
                return OrderSide.Sell;
            }

            if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
                return OrderSide.Sell;
            return OrderSide.Buy;
        }

        #region Get Futures Trigger Order

        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                SharedOrderType.Limit,
                order.Data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.OrderStatus),
                order.Data.OrderConfiguration.StopPrice ?? 0,
                null,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.OrderConfiguration.Quantity, quoteAssetQuantity: order.Data.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: order.Data.QuantityFilled, quoteAssetQuantity: order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        #endregion

        #region Cancel Futures Trigger Order

        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

    }
}
