using CryptoExchange.Net.SharedApis;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Shared interface for the Advanced Trade rest API
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ITradeHistoryRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        ISpotOrderRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        IOpenInterestRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IFeeRestClient,
        ISpotTriggerOrderRestClient,
        IFuturesTriggerOrderRestClient,
        IBookTickerRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeSharedApi :
        IGetAllAssetsRest,
        IGetAssetRest,
        IGetBalancesRest,
        IGetDepositAddressesRest,
        IGetDepositHistoryRest,
        IGetOrderBookRest,
        IGetRecentTradesRest,
        IGetTradeHistoryRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,
        IGetSpotSymbolsRest,
        IGetSpotTickerRest,
        IGetAllSpotTickersRest,
        IPlaceSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        IGetSpotUserTradeHistoryRest,
        ICancelSpotOrderRest,
        IGetFuturesSymbolsRest,
        IGetFuturesTickerRest,
        IGetAllFuturesTickersRest,
        IGetOpenInterestRest,
        IPlaceFuturesOrderRest,
        IGetFuturesOrderRest,
        IGetOpenFuturesOrdersRest,
        IGetClosedFuturesOrdersRest,
        IGetFuturesOrderTradesRest,
        IGetFuturesUserTradeHistoryRest,
        ICancelFuturesOrderRest,
        IGetPositionsRest,
        IClosePositionRest,
        IGetKlinesRest,
        IGetFeesRest,
        IPlaceSpotTriggerOrderRest,
        IGetSpotTriggerOrderRest,
        ICancelSpotTriggerOrderRest,
        IPlaceFuturesTriggerOrderRest,
        IGetFuturesTriggerOrderRest,
        ICancelFuturesTriggerOrderRest,
        IGetBookTickerRest
    {
    }
}
