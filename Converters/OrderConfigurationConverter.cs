using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Converters
{
    internal class OrderConfigurationConverter : JsonConverter<CoinbaseOrderConfiguration>
    {
        public override CoinbaseOrderConfiguration? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var result = new CoinbaseOrderConfiguration();
            if (jsonDoc.RootElement.TryGetProperty("market_market_ioc", out var marketElement))
            {
                if (marketElement.TryGetProperty("quote_size", out var quoteSize))
                    result.QuoteQuantity = decimal.Parse(quoteSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (marketElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("sor_limit_ioc", out var limitIocElement))
            {
                if (limitIocElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitIocElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("limit_limit_gtc", out var limitGtcElement))
            {
                if (limitGtcElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtcElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtcElement.TryGetProperty("post_only", out var postOnly))
                    result.PostOnly = postOnly.GetBoolean();
            }
            else if (jsonDoc.RootElement.TryGetProperty("limit_limit_gtd", out var limitGtdElement))
            {
                if (limitGtdElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtdElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtdElement.TryGetProperty("post_only", out var postOnly))
                    result.PostOnly = postOnly.GetBoolean();
                if (limitGtdElement.TryGetProperty("end_time", out var endTime))
                    result.CancelTime = endTime.GetDateTime();
            }
            else if (jsonDoc.RootElement.TryGetProperty("limit_limit_fok", out var limitFokElement))
            {
                if (limitFokElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitFokElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("stop_limit_stop_limit_gtc", out var stopLimitGtcElement))
            {
                if (stopLimitGtcElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtcElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtcElement.TryGetProperty("stop_price", out var stopPrice))
                    result.StopPrice = decimal.Parse(stopPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtcElement.TryGetProperty("stop_direction", out var stopDirection))
                    result.StopDirection = EnumConverter.ParseString<StopDirection>(stopDirection.GetString()!);
            }
            else if (jsonDoc.RootElement.TryGetProperty("stop_limit_stop_limit_gtd", out var stopLimitGtdElement))
            {
                if (stopLimitGtdElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtdElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtdElement.TryGetProperty("stop_price", out var stopPrice))
                    result.StopPrice = decimal.Parse(stopPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtdElement.TryGetProperty("end_time", out var endTime))
                    result.CancelTime = endTime.GetDateTime();
                if (stopLimitGtdElement.TryGetProperty("stop_direction", out var stopDirection))
                    result.StopDirection = EnumConverter.ParseString<StopDirection>(stopDirection.GetString()!);
            }
            else if (jsonDoc.RootElement.TryGetProperty("trigger_bracket_gtc", out var triggerGtcElement))
            {
                if (triggerGtcElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtcElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtcElement.TryGetProperty("stop_trigger_price", out var stopTriggerPrice))
                    result.StopPrice = decimal.Parse(stopTriggerPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("trigger_bracket_gtd", out var triggerGtdElement))
            {
                if (triggerGtdElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtdElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtdElement.TryGetProperty("stop_trigger_price", out var stopTriggerPrice))
                    result.StopPrice = decimal.Parse(stopTriggerPrice.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtdElement.TryGetProperty("end_time", out var endTime))
                    result.CancelTime = endTime.GetDateTime();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, CoinbaseOrderConfiguration value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
