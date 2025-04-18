using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Intraday margin setting
    /// </summary>
    [SerializationModel]
    public record IntradayMarginSetting
    {
        /// <summary>
        /// Intraday trading setting value
        /// </summary>
        [JsonPropertyName("setting")]
        public IntradayMargin Setting { get; set; }
    }


}
