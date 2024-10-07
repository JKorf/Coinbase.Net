using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

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
        IFuturesOrderRestClient
    {
    }
}