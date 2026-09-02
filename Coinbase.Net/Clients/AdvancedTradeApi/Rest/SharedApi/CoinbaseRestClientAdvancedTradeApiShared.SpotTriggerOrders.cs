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
        #region Spot Trigger Order Client
        public PlaceSpotTriggerOrderOptions PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, true);

        public async Task<HttpResult<SharedId>> PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(                
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                NewOrderType.StopLimit,
                quantity: request.Quantity?.QuantityInBaseAsset,
                quoteQuantity: request.Quantity?.QuantityInQuoteAsset,
                // Simulate market order by adding/removing 10% to the trigger price as order price
                price: request.OrderPrice ?? (request.TriggerPrice + (request.TriggerPrice * 0.1m * (request.OrderSide == SharedOrderSide.Buy ? 1 : -1))),
                stopPrice: request.TriggerPrice,
                clientOrderId: request.ClientOrderId,
                stopDirection: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopDirection.Up : StopDirection.Down,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        public GetSpotTriggerOrderOptions GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        public async Task<HttpResult<SharedSpotTriggerOrder>> GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await _api.Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                SharedOrderType.Limit,
                order.Data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.OrderStatus),
                order.Data.OrderConfiguration.StopPrice ?? 0,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.OrderConfiguration.Quantity, order.Data.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(OrderStatus orderStatus)
        {
            if (orderStatus == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (orderStatus == OrderStatus.Canceled
                || orderStatus == OrderStatus.Expired
                || orderStatus == OrderStatus.Failed)
            {
                return SharedTriggerOrderStatus.CanceledOrRejected;
            }

            if (orderStatus == OrderStatus.Open
                || orderStatus == OrderStatus.CancelQueued
                || orderStatus == OrderStatus.Pending)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        public CancelSpotTriggerOrderOptions CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
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
