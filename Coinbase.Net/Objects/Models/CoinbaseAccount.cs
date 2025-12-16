using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseAccountWrapper
    {
        [JsonPropertyName("account")]
        public CoinbaseAccount Account { get; set; } = null!;
    }

    /// <summary>
    /// Accounts page
    /// </summary>
    [SerializationModel]
    public record CoinbaseAccountPage : CoinbasePage
    {
        /// <summary>
        /// Accounts
        /// </summary>
        [JsonPropertyName("accounts")]
        public CoinbaseAccount[] Accounts { get; set; } = Array.Empty<CoinbaseAccount>();
    }

    /// <summary>
    /// Account/balance info
    /// </summary>
    [SerializationModel]
    public record CoinbaseAccount
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("uuid")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Account name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public CoinbaseQuantityReference AvailableBalance { get; set; } = null!;
        /// <summary>
        /// Holding/frozen balance
        /// </summary>
        [JsonPropertyName("hold")]
        public CoinbaseQuantityReference HoldBalance { get; set; } = null!;
        /// <summary>
        /// Type of account
        /// </summary>
        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// Default
        /// </summary>
        [JsonPropertyName("default")]
        public bool Default { get; set; }
        /// <summary>
        /// Active
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        /// <summary>
        /// Ready
        /// </summary>
        [JsonPropertyName("ready")]
        public bool Ready { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Delete time
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// Portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string PortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// Account platform
        /// </summary>
        [JsonPropertyName("platform")]
        public AccountPlatform Platform { get; set; }
    }

    /// <summary>
    /// Quantity
    /// </summary>
    [SerializationModel]
    public record CoinbaseQuantityReference
    {
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        [JsonPropertyName("amount")]
        [JsonInclude]
        internal decimal Amount { set => Value = value; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
