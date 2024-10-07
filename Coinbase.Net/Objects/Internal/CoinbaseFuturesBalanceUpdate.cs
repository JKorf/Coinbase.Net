﻿using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseFuturesBalanceUpdate : CoinbaseSocketEvent
    {
        [JsonPropertyName("fcm_balance_summary")]
        public CoinbaseFuturesBalance BalanceSummary { get; set; } = null!;
    }
}