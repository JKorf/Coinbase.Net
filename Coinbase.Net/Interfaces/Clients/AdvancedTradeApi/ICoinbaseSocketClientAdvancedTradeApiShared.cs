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
}
