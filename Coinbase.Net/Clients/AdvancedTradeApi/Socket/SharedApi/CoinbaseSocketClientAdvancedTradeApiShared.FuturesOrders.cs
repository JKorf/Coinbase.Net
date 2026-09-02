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
        #region Futures Order client

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserUpdatesAsync(
                update =>
                {
                    var orders = update.Data.Orders.Where(x => x.SymbolType == SymbolType.Futures).Select(x =>
                        new SharedFuturesOrderUpdate(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            x.OrderId.ToString(),
                            x.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : x.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                            x.OrderSide == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(x.Status),
                            x.CreateTime)
                        {
                            ClientOrderId = x.ClientOrderId,
                            OrderPrice = x.Price == 0 ? null : x.Price,
                            AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                            OrderQuantity = new SharedOrderQuantity(contractQuantity: x.QuantityFilled + x.QuantityRemaining),
                            QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.ValueFilled, contractQuantity: x.QuantityFilled),
#pragma warning disable CS0618 // Type or member is obsolete
                            Fee = x.TotalFees,
#pragma warning restore CS0618 // Type or member is obsolete
                            TimeInForce = x.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : x.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                            IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
                        }).ToArray();

                    if (!orders.Any())
                        return;

                    handler(update.ToType<SharedFuturesOrderUpdate[]>(orders));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
