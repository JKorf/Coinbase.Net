using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;
using Coinbase.Net.Interfaces.Clients.SpotApi;

namespace Coinbase.Net.Clients.SpotApi
{
    internal partial class CoinbaseSocketClientAdvancedTradeApi : ICoinbaseSocketClientAdvancedTradeApiShared
    {
        public string Exchange => "Coinbase";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
    }
}
