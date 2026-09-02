using CryptoExchange.Net.SharedApis;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Shared interface for the Advanced Trade socket API
    /// </summary>
    public interface ICoinbaseSocketClientAdvancedTradeApiShared :
        IKlineSocketClient,
        ITickerSocketClient,
        ITradeSocketClient,
        ISpotOrderSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface ICoinbaseSocketClientAdvancedTradeSharedApi :
        ISubscribeKlinesSocket,
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket
    { }
}
