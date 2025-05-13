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
        /// Online
        /// </summary>
        [Map("online", "STANDARD")]
        Online,
        /// <summary>
        /// Offline
        /// </summary>
        [Map("offline")]
        Offline,
        /// <summary>
        /// Internal
        /// </summary>
        [Map("internal")]
        Internal,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("delisted")]
        Delisted,
        /// <summary>
        /// Pre-launch
        /// </summary>
        [Map("PRE_LAUNCH")]
        PreLaunch
    }
}
