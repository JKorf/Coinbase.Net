using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{

    /// <summary>
    /// Resource reference
    /// </summary>
    public record CoinbaseResourceReference
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;
        /// <summary>
        /// Resource
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;
        /// <summary>
        /// Resource path
        /// </summary>
        [JsonPropertyName("resource_path")]
        public string? ResourcePath { get; set; }
    }

    /// <summary>
    /// Reference
    /// </summary>
    public record CoinbaseToReference : CoinbaseResourceReference
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
