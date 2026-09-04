using CryptoExchange.Net.SharedApis;
using System;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Objects;
using System.Linq;
using Coinbase.Net.Enums;
using CryptoExchange.Net;
using Coinbase.Net.Objects.Models;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    internal partial class CoinbaseSocketClientAdvancedTradeSharedApi
    {

        #region Subscribe To Spot Order Updates

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserUpdatesAsync(
                update =>
                {
                    var orders = update.Data.Orders.Where(x => x.SymbolType == SymbolType.Spot).Select(x =>
                        new SharedSpotOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            ParseOrderType(x.OrderType),
                            x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.Status),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                            OrderQuantity = ParseOrderQuantity(x),
                            QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.ValueFilled),
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = x.TotalFees,
#pragma warning restore CS0618 // Type or member is obsolete
                            TriggerPrice = x.StopPrice,
                            TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                            IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
                        }).ToArray();

                    if (!orders.Any())
                        return;

                    handler(update.ToType<SharedSpotOrderUpdate[]>(orders));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedOrderQuantity ParseOrderQuantity(CoinbaseOrderUpdate order)
        {
            if (order.OrderType != OrderType.Market || order.OrderSide != OrderSide.Buy)
                return new SharedOrderQuantity(order.QuantityFilled + order.QuantityRemaining); // Always base asset quantity

            // Can be either base or quote asset quantity, but not very clear how to know
            // If the total value of the order is the same as the quantity we assume order quantity is in quote
            if (order.TotalValueAfterFees == (order.QuantityFilled + order.QuantityRemaining))
                return new SharedOrderQuantity(null, order.QuantityFilled + order.QuantityRemaining);

            return new SharedOrderQuantity(order.QuantityFilled + order.QuantityRemaining);
        }

        private SharedOrderType ParseOrderType(OrderType orderType)
        {
            if (orderType == OrderType.Market || orderType == OrderType.Stop)
                return SharedOrderType.Market;

            if (orderType == OrderType.Limit || orderType == OrderType.StopLimit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Pending || status == OrderStatus.Open || status == OrderStatus.Queued || status == OrderStatus.CancelQueued) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.Expired || status == OrderStatus.Failed) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Filled) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }
    }
}
