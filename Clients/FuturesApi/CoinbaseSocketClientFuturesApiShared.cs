using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;
using Coinbase.Net.Interfaces.Clients.FuturesApi;

namespace Coinbase.Net.Clients.FuturesApi
{
    internal partial class CoinbaseSocketClientFuturesApi : ICoinbaseSocketClientFuturesApiShared
    {
        public string Exchange => "Coinbase";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
    }
}
