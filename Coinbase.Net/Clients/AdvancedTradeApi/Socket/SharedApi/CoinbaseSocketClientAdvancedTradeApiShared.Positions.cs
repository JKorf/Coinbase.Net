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

        public SubscribePositionOptions SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true);
        #region Subscribe To Position Updates

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserUpdatesAsync(
                update =>
                {
                    var positions = update.Data.PositionInfo.PerpetualPositions.Select(x =>
                        new SharedPosition(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol,
                            new SharedOrderQuantity(x.NetQuantity),
                            null)
                        {
                            AverageOpenPrice = x.EntryVolumeWeightedAveragePrice,
                            Leverage = x.Leverage,
                            LiquidationPrice = x.LiquidationPrice,
                            PositionMode = SharedPositionMode.HedgeMode,
                            PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                            UnrealizedPnl = x.UnrealizedPnl
                        }).ToList();

                    positions.AddRange(update.Data.PositionInfo.ExpiringPositions.Select(x =>
                        new SharedPosition(
                            ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                            x.Symbol, 
                            new SharedOrderQuantity(x.NumberOfContracts),
                            null)
                            {
                                AverageOpenPrice = x.EntryPrice,
                                PositionMode = SharedPositionMode.HedgeMode,
                                PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                                UnrealizedPnl = x.UnrealizedPnl
                            }));

                    handler(update.ToType<SharedPosition[]>(positions.ToArray()));
                },
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
