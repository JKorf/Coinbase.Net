using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePaymentMethodsWrapper
    {
        /// <summary>
        /// Payment methods
        /// </summary>
        [JsonPropertyName("payment_methods")]
        public CoinbasePaymentMethod[] PaymentMethods { get; set; } = null!;
    }

    [SerializationModel]
    internal record CoinbasePaymentMethodWrapper
    {
        /// <summary>
        /// Payment methods
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CoinbasePaymentMethod PaymentMethod { get; set; } = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CoinbasePaymentMethod
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Verified
        /// </summary>
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
        /// <summary>
        /// Allow buy
        /// </summary>
        [JsonPropertyName("allow_buy")]
        public bool AllowBuy { get; set; }
        /// <summary>
        /// Allow sell
        /// </summary>
        [JsonPropertyName("allow_sell")]
        public bool AllowSell { get; set; }
        /// <summary>
        /// Allow deposit
        /// </summary>
        [JsonPropertyName("allow_deposit")]
        public bool AllowDeposit { get; set; }
        /// <summary>
        /// Allow withdraw
        /// </summary>
        [JsonPropertyName("allow_withdraw")]
        public bool AllowWithdraw { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdateTime { get; set; }
    }


}
