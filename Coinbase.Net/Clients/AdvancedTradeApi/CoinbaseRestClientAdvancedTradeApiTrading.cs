using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using Coinbase.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net;
using Coinbase.Net.Objects.Models;
using System.Collections.Generic;
using System.Linq;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using CryptoExchange.Net.Objects.Errors;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientAdvancedTradeApiTrading : ICoinbaseRestClientAdvancedTradeApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CoinbaseRestClientAdvancedTradeApi _baseClient;
        private readonly ILogger _logger;

        internal CoinbaseRestClientAdvancedTradeApiTrading(ILogger logger, CoinbaseRestClientAdvancedTradeApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseOrderResult>> PlaceOrderAsync(
            string symbol,
            OrderSide side, 
            NewOrderType orderType,
            decimal? quantity = null,
            decimal? quoteQuantity = null, 
            decimal? price = null,
            string? clientOrderId = null,
            decimal? leverage = null,
            MarginType? marginType = null,
            string? previewId = null,
            bool? postOnly = null,
            DateTime? cancelTime = null,
            decimal? stopPrice = null,
            StopDirection? stopDirection = null,
            decimal? attachedOrderTriggerPrice = null,
            decimal? attachedOrderLimitPrice = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("product_id", symbol);
            parameters.Add("side", side);
            parameters.Add("leverage", leverage);
            parameters.Add("margin_type", marginType);
            parameters.Add("preview_id", previewId);
            parameters.Add("client_order_id", clientOrderId ?? ExchangeHelpers.RandomString(24));

            var marketConfig = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            marketConfig.Add("base_size", quantity);
            marketConfig.Add("quote_size", quoteQuantity);
            marketConfig.Add("limit_price", price);
            marketConfig.Add("post_only", postOnly);
            marketConfig.Add("end_time", cancelTime);
            marketConfig.Add("stop_direction", stopDirection);
            if (orderType == NewOrderType.Bracket || orderType == NewOrderType.BracketGoodTillDate)
                marketConfig.Add("stop_trigger_price", stopPrice);
            else
                marketConfig.Add("stop_price", stopPrice);

            var wrapper = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            wrapper.Add(EnumConverter.GetString(orderType), marketConfig);
            parameters.Add("order_configuration", wrapper);

            if (attachedOrderTriggerPrice != null)
            {
                var attachedConfig = new Parameters(CoinbaseExchange._parameterSerializationSettings);
                attachedConfig.Add("stopTriggerPrice", attachedOrderTriggerPrice);
                attachedConfig.Add("limitPrice", attachedOrderLimitPrice);
                parameters.Add("attached_order_configuration", new Dictionary<string, object>
                {
                    { "triggerBracketGtc", attachedConfig }
                });
            }

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/brokerage/orders", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrderResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (!result.Data.Success && result.Data.ErrorResponse != null)
            {
                var errorMessage = result.Data.ErrorResponse.Message;
                if (!string.IsNullOrEmpty(result.Data.ErrorResponse.PreviewFailureReason))
                    errorMessage = result.Data.ErrorResponse.PreviewFailureReason;
                else if (!string.IsNullOrEmpty(result.Data.ErrorResponse.OrderFailureReason))
                    errorMessage = result.Data.ErrorResponse.PreviewFailureReason;
                return HttpResult.Fail<CoinbaseOrderResult>(result, new ServerError(result.Data.ErrorResponse.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorResponse.ErrorCode, errorMessage)));
            }

            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseCancelResult>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            var result = await CancelOrdersAsync(new[] { orderId }, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseCancelResult>(result);

            var cancelResult = result.Data.SingleOrDefault();
            if (cancelResult == null)
                return HttpResult.Fail<CoinbaseCancelResult>(result, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Order not found")));

            if (!cancelResult.Success)
                return HttpResult.Fail<CoinbaseCancelResult>(result, new ServerError(new ErrorInfo(ErrorType.Unknown, cancelResult.ErrorMessage!)));

            return HttpResult.Ok(result, cancelResult);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseCancelResult[]>> CancelOrdersAsync(IEnumerable<string> orderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("order_ids", orderIds.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/brokerage/orders/batch_cancel", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseCancelResultWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseCancelResult[]>(result);
            return HttpResult.Ok(result, result.Data.Results);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseEditOrderResult>> EditOrderAsync(string orderId, decimal price, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("order_id", orderId);
            parameters.Add("price", price);
            parameters.Add("size", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/brokerage/orders/edit", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseEditOrderResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (!result.Data.Success && result.Data.ErrorResponse != null)
                return HttpResult.Fail<CoinbaseEditOrderResult>(result, new ServerError(result.Data.ErrorResponse.ErrorCode, _baseClient.GetErrorInfo(result.Data.ErrorResponse.ErrorCode, result.Data.ErrorResponse.Message)));

            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v3/brokerage/orders/historical/{orderId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseOrder>(result);
            return HttpResult.Ok(result, result.Data.Order);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseOrder[]>> GetOrdersAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? symbols = null,
            SymbolType? symbolType = null,
            IEnumerable<OrderStatus>? orderStatus = null,
            IEnumerable<TimeInForce>? timeInForces = null,
            IEnumerable<OrderType>? orderTypes = null,
            OrderSide? orderSide = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            string? orderSource = null,
            ContractExpiryType? expiryType = null,
            IEnumerable<string>? assets = null,
            int? limit = null,
            string? cursor = null,
            OrderSortBy? sortBy = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.AddArray("order_ids", orderIds?.ToArray());
            parameters.AddArray("product_ids", symbols?.ToArray());
            parameters.Add("product_type", symbolType);
            parameters.AddArray("order_status", orderStatus?.Select(EnumConverter.GetString).ToArray());
            parameters.AddArray("time_in_forces", timeInForces?.Select(EnumConverter.GetString).ToArray());
            parameters.AddArray("order_types", orderTypes?.Select(EnumConverter.GetString).ToArray());
            parameters.Add("order_side", orderSide);
            parameters.Add("start_date", startTime?.ToRfc3339String());
            parameters.Add("end_date", endTime?.ToRfc3339String());
            parameters.Add("order_placement_source", orderSource);
            parameters.Add("contract_expiry_type", expiryType);
            parameters.AddArray("asset_filters", assets?.ToArray());
            parameters.Add("limit", limit);
            parameters.Add("cursor", cursor);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/brokerage/orders/historical/batch", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrdersWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseOrder[]>(result);
            return HttpResult.Ok(result, result.Data.Orders);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseUserTrades>> GetUserTradesAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? tradeIds = null,
            IEnumerable<string>? symbols = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            TradeSortBy? sortBy = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.AddArray("order_ids", orderIds?.ToArray());
            parameters.AddArray("trade_ids", tradeIds?.ToArray());
            parameters.AddArray("product_ids", symbols?.ToArray());
            parameters.Add("start_sequence_timestamp", startTime?.ToRfc3339String());
            parameters.Add("end_sequence_timestamp", endTime?.ToRfc3339String());
            parameters.Add("limit", limit);
            parameters.Add("cursor", cursor);
            parameters.Add("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/brokerage/orders/historical/fills", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseUserTrades>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseOrderResult>> ClosePositionAsync(string symbol, decimal? quantity = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("product_id", symbol);
            parameters.Add("client_order_id", clientOrderId ?? ExchangeHelpers.RandomString(24));
            parameters.Add("size", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/api/v3/brokerage/orders/close_position", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Positions

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseFuturesPosition[]>> GetFuturesPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/brokerage/cfm/positions", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesPositionsWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseFuturesPosition[]>(result);
            return HttpResult.Ok(result, result.Data.Positions);
        }

        #endregion

        #region Get Futures Position

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseFuturesPosition>> GetFuturesPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v3/brokerage/cfm/positions/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseFuturesPosition>(result);
            return HttpResult.Ok(result, result.Data.Position);
        }

        #endregion

        #region Get Perpetual Positions

        /// <inheritdoc />
        public async Task<HttpResult<CoinbasePerpetualPositions>> GetPerpetualPositionsAsync(string portfolioId, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/intx/positions/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPositions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Perpetual Position

        /// <inheritdoc />
        public async Task<HttpResult<CoinbasePerpetualPosition>> GetPerpetualPositionAsync(string portfolioId, string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/intx/positions/{portfolioId}/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbasePerpetualPosition>(result);
            return HttpResult.Ok(result, result.Data.Position);
        }

        #endregion
    }
}
