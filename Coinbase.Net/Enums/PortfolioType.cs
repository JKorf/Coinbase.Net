using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Portfolio type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PortfolioType>))]
    public enum PortfolioType
    {
        /// <summary>
        /// ["<c>UNDEFINED</c>"] Undefined
        /// </summary>
        [Map("UNDEFINED")]
        Undefined,
        /// <summary>
        /// ["<c>DEFAULT</c>"] Default
        /// </summary>
        [Map("DEFAULT")]
        Default,
        /// <summary>
        /// ["<c>CONSUMER</c>"] Consumer
        /// </summary>
        [Map("CONSUMER")]
        Consumer,
        /// <summary>
        /// ["<c>INTX</c>"] International
        /// </summary>
        [Map("INTX")]
        International
    }
}
