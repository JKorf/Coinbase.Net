using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Book update
    /// </summary>
    public record CoinbaseExBookUpdate : CoinbaseSocketEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Change
        /// </summary>
        [JsonPropertyName("changes")]
        public CoinbaseExOrderBookEntryChange[] Change { get; set; } = [];
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<CoinbaseExOrderBookEntryChange>))]
    public record CoinbaseExOrderBookEntryChange
    {
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(EnumConverter<OrderSide>))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(1)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [ArrayProperty(2)]
        public decimal Quantity { get; set; }
    }
}
