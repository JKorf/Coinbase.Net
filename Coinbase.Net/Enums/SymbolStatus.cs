using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>online</c>"] Online
        /// </summary>
        [Map("online", "STANDARD")]
        Online,
        /// <summary>
        /// ["<c>offline</c>"] Offline
        /// </summary>
        [Map("offline")]
        Offline,
        /// <summary>
        /// ["<c>internal</c>"] Internal
        /// </summary>
        [Map("internal")]
        Internal,
        /// <summary>
        /// ["<c>delisted</c>"] Delisted
        /// </summary>
        [Map("delisted")]
        Delisted,
        /// <summary>
        /// ["<c>PRE_LAUNCH</c>"] Pre-launch
        /// </summary>
        [Map("PRE_LAUNCH")]
        PreLaunch
    }
}
