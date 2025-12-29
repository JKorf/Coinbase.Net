using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models;

/// <summary>
/// Timestamp response
/// </summary>
[SerializationModel]
public class CoinbaseExchangeTime
{
    /// <summary>
    /// Current time
    /// </summary>
    [JsonPropertyName("epoch")]
    public DateTime Time { get; set; }
}