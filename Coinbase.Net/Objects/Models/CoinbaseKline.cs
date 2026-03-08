using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>start</c>"] Candle open time
        /// </summary>
        [JsonPropertyName("start")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>low</c>"] Low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>high</c>"] High price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>open</c>"] Open price
        /// </summary>
        [JsonPropertyName("open")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>close</c>"] Close price
        /// </summary>
        [JsonPropertyName("close")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
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
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
    }


}
