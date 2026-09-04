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

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        #region Subscribe To Trade Updates

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)) : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToTradeUpdatesAsync(symbols, update =>
            {
                if (update.UpdateType == SocketUpdateType.Snapshot)
                    return;

                foreach (var item in update.Data)
                {
                    handler(update.ToType<SharedTrade[]>(new[] { 
                        new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicSpotId, _api.EnvironmentName, null, item.Symbol), item.Symbol, new SharedOrderQuantity(item.Quantity), item.Price, item.Timestamp){
                        Side = item.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                    } }));
                }

            }, ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
