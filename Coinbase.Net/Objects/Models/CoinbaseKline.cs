using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseKlineWrapper
    {
        [JsonPropertyName("candles")]
        public CoinbaseKline[] Klines { get; set; } = Array.Empty<CoinbaseKline>();
    }

    /// <summary>
    /// Kline data
    /// </summary>
    [SerializationModel]
    public record CoinbaseKline
    {
        /// <summary>
        /// Candle open time
        /// </summary>
        [JsonPropertyName("start")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
    }

    /// <summary>
    /// Stream kline
    /// </summary>
    [SerializationModel]
    public record CoinbaseStreamKline: CoinbaseKline
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
    }


}
