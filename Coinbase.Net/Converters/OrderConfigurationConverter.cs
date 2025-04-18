using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net;
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
                result.OrderType = NewOrderType.Market;
                if (marketElement.TryGetProperty("quote_size", out var quoteSize))
                    result.QuoteQuantity = decimal.Parse(quoteSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (marketElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("sor_limit_ioc", out var limitIocElement))
            {
                result.OrderType = NewOrderType.LimitImmediateOrCancel;
                if (limitIocElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitIocElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("limit_limit_gtc", out var limitGtcElement))
            {
                result.OrderType = NewOrderType.Limit;
                if (limitGtcElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtcElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtcElement.TryGetProperty("post_only", out var postOnly))
                    result.PostOnly = postOnly.GetBoolean();
            }
            else if (jsonDoc.RootElement.TryGetProperty("limit_limit_gtd", out var limitGtdElement))
            {
                result.OrderType = NewOrderType.LimitGoodTillDate;
                if (limitGtdElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtdElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitGtdElement.TryGetProperty("post_only", out var postOnly))
                    result.PostOnly = postOnly.GetBoolean();
                if (limitGtdElement.TryGetProperty("end_time", out var endTime))
                    result.CancelTime = endTime.GetDateTime();
            }
            else if (jsonDoc.RootElement.TryGetProperty("limit_limit_fok", out var limitFokElement))
            {
                result.OrderType = NewOrderType.LimitFillOrKill;
                if (limitFokElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (limitFokElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("stop_limit_stop_limit_gtc", out var stopLimitGtcElement))
            {
                result.OrderType = NewOrderType.StopLimit;
                if (stopLimitGtcElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtcElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtcElement.TryGetProperty("stop_price", out var stopPrice))
                    result.StopPrice = decimal.Parse(stopPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtcElement.TryGetProperty("stop_direction", out var stopDirection))
                    result.StopDirection = EnumConverter.ParseString<StopDirection>(stopDirection.GetString()!);
            }
            else if (jsonDoc.RootElement.TryGetProperty("stop_limit_stop_limit_gtd", out var stopLimitGtdElement))
            {
                result.OrderType = NewOrderType.StopLimitGoodTillDate;
                if (stopLimitGtdElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtdElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtdElement.TryGetProperty("stop_price", out var stopPrice))
                    result.StopPrice = decimal.Parse(stopPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (stopLimitGtdElement.TryGetProperty("end_time", out var endTime))
                    result.CancelTime = endTime.GetDateTime();
                if (stopLimitGtdElement.TryGetProperty("stop_direction", out var stopDirection))
                    result.StopDirection = EnumConverter.ParseString<StopDirection>(stopDirection.GetString()!);
            }
            else if (jsonDoc.RootElement.TryGetProperty("trigger_bracket_gtc", out var triggerGtcElement))
            {
                result.OrderType = NewOrderType.Bracket;
                if (triggerGtcElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtcElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtcElement.TryGetProperty("stop_trigger_price", out var stopTriggerPrice))
                    result.StopPrice = decimal.Parse(stopTriggerPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (jsonDoc.RootElement.TryGetProperty("trigger_bracket_gtd", out var triggerGtdElement))
            {
                result.OrderType = NewOrderType.BracketGoodTillDate;
                if (triggerGtdElement.TryGetProperty("base_size", out var baseSize))
                    result.Quantity = decimal.Parse(baseSize.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtdElement.TryGetProperty("limit_price", out var limitPrice))
                    result.Price = decimal.Parse(limitPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtdElement.TryGetProperty("stop_trigger_price", out var stopTriggerPrice))
                    result.StopPrice = decimal.Parse(stopTriggerPrice.GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture);
                if (triggerGtdElement.TryGetProperty("end_time", out var endTime))
                    result.CancelTime = endTime.GetDateTime();
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, CoinbaseOrderConfiguration value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.OrderType == NewOrderType.Market)
            {
                writer.WritePropertyName("market_market_ioc");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.QuoteQuantity.HasValue)
                    writer.WriteString("quote_size", value.QuoteQuantity.Value.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.LimitImmediateOrCancel)
            {
                writer.WritePropertyName("sor_limit_ioc");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.Limit)
            {
                writer.WritePropertyName("limit_limit_gtc");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));
                if (value.PostOnly.HasValue)
                    writer.WriteBoolean("post_only", value.PostOnly.Value);

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.LimitGoodTillDate)
            {
                writer.WritePropertyName("limit_limit_gtd");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));
                if (value.PostOnly.HasValue)
                    writer.WriteBoolean("post_only", value.PostOnly.Value);
                if (value.CancelTime.HasValue)
                    writer.WriteString("end_time", value.CancelTime.Value.ToRfc3339String());

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.LimitFillOrKill)
            {
                writer.WritePropertyName("limit_limit_fok");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.StopLimit)
            {
                writer.WritePropertyName("stop_limit_stop_limit_gtc");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));
                if (value.StopPrice.HasValue)
                    writer.WriteString("stop_price", value.StopPrice.Value.ToString(CultureInfo.InvariantCulture));
                if (value.StopDirection.HasValue)
                    writer.WriteString("stop_direction", EnumConverter.GetString(value.StopDirection.Value));

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.StopLimitGoodTillDate)
            {
                writer.WritePropertyName("stop_limit_stop_limit_gtd");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));
                if (value.StopPrice.HasValue)
                    writer.WriteString("stop_price", value.StopPrice.Value.ToString(CultureInfo.InvariantCulture));
                if (value.StopDirection.HasValue)
                    writer.WriteString("stop_direction", EnumConverter.GetString(value.StopDirection.Value));
                if (value.CancelTime.HasValue)
                    writer.WriteString("end_time", value.CancelTime.Value.ToRfc3339String());

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.Bracket)
            {
                writer.WritePropertyName("trigger_bracket_gtc");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));
                if (value.StopPrice.HasValue)
                    writer.WriteString("stop_trigger_price", value.StopPrice.Value.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndObject();
            }
            else if (value.OrderType == NewOrderType.BracketGoodTillDate)
            {
                writer.WritePropertyName("trigger_bracket_gtd");
                writer.WriteStartObject();

                if (value.Quantity.HasValue)
                    writer.WriteString("base_size", value.Quantity.Value.ToString(CultureInfo.InvariantCulture));
                if (value.Price.HasValue)
                    writer.WriteString("limit_price", value.Price.Value.ToString(CultureInfo.InvariantCulture));
                if (value.StopPrice.HasValue)
                    writer.WriteString("stop_trigger_price", value.StopPrice.Value.ToString(CultureInfo.InvariantCulture));
                if (value.CancelTime.HasValue)
                    writer.WriteString("end_time", value.CancelTime.Value.ToRfc3339String());

                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }
    }
}
