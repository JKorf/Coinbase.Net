using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record CoinbaseExAsset
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Min size
        /// </summary>
        [JsonPropertyName("min_size")]
        public decimal MinSize { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Status info
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }
        /// <summary>
        /// Max precision
        /// </summary>
        [JsonPropertyName("max_precision")]
        public decimal MaxPrecision { get; set; }
        /// <summary>
        /// Convertible to
        /// </summary>
        [JsonPropertyName("convertible_to")]
        public string[] ConvertibleTo { get; set; } = [];
        /// <summary>
        /// Default network
        /// </summary>
        [JsonPropertyName("default_network")]
        public string DefaultNetwork { get; set; } = string.Empty;
        /// <summary>
        /// Supported networks
        /// </summary>
        [JsonPropertyName("supported_networks")]
        public CoinbaseExAssetNetwork[] SupportedNetworks { get; set; } = [];
    }

    /// <summary>
    /// Asset network
    /// </summary>
    public record CoinbaseExAssetNetwork
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Crypto address link
        /// </summary>
        [JsonPropertyName("crypto_address_link")]
        public string AddressLink { get; set; } = string.Empty;
        /// <summary>
        /// Crypto transaction link
        /// </summary>
        [JsonPropertyName("crypto_transaction_link")]
        public string TransactionLink { get; set; } = string.Empty;
        /// <summary>
        /// Min withdraw quantity
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Network confirmations required
        /// </summary>
        [JsonPropertyName("network_confirmations")]
        public int NetworkConfirmations { get; set; }
        /// <summary>
        /// Processing time in seconds
        /// </summary>
        [JsonPropertyName("processing_time_seconds")]
        public int ProcessingTime { get; set; }
        /// <summary>
        /// Destination tag regex
        /// </summary>
        [JsonPropertyName("destination_tag_regex")]
        public string DestinationTagRegex { get; set; } = string.Empty;
    }
}
