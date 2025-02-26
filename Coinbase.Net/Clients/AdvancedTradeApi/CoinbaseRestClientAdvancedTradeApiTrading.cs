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
        public async Task<WebCallResult<CoinbaseOrderResult>> PlaceOrderAsync(
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
            DateTime? twapStartTime = null,
            DateTime? twapEndTime = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("product_id", symbol);
            parameters.AddEnum("side", side);
            parameters.AddOptionalString("leverage", leverage);
            parameters.AddOptionalEnum("margin_type", marginType);
            parameters.AddOptional("preview_id", previewId);
            parameters.Add("client_order_id", clientOrderId ?? ExchangeHelpers.RandomString(24));

            var marketConfig = new ParameterCollection();
            marketConfig.AddOptionalString("base_size", quantity);
            marketConfig.AddOptionalString("quote_size", quoteQuantity);
            marketConfig.AddOptionalString("limit_price", price);
            marketConfig.AddOptional("post_only", postOnly);
            marketConfig.AddOptional("start_time", twapStartTime?.ToString("yyy-MM-ddThh:mm:ssZ"));
            marketConfig.AddOptional("end_time", twapEndTime?.ToString("yyy-MM-ddThh:mm:ssZ") ?? cancelTime?.ToString("yyy-MM-ddThh:mm:ssZ"));
            marketConfig.AddOptionalEnum("stop_direction", stopDirection);
            if (orderType == NewOrderType.Bracket || orderType == NewOrderType.BracketGoodTillDate)
                marketConfig.AddOptionalString("stop_trigger_price", stopPrice);
            else
                marketConfig.AddOptionalString("stop_price", stopPrice);

            var wrapper = new ParameterCollection();
            wrapper.Add(EnumConverter.GetString(orderType), marketConfig);
            parameters.Add("order_configuration", wrapper);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/brokerage/orders", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrderResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result;

            if (!result.Data.Success && result.Data.ErrorResponse != null)
                return result.AsError<CoinbaseOrderResult>(new ServerError($"{result.Data.ErrorResponse.ErrorCode}: {result.Data.ErrorResponse.Message}"));

            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseCancelResult>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        {
            var result = await CancelOrdersAsync(new[] { orderId }, ct).ConfigureAwait(false);
            if (!result)
                return result.As<CoinbaseCancelResult>(default);

            var cancelResult = result.Data.SingleOrDefault();
            if (cancelResult == null)
                return result.AsError<CoinbaseCancelResult>(new ServerError("Order not found"));

            if (!cancelResult.Success)
                return result.AsError<CoinbaseCancelResult>(new ServerError(cancelResult.ErrorMessage!));

            return result.As(cancelResult);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseCancelResult>>> CancelOrdersAsync(IEnumerable<string> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("order_ids", orderIds.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/brokerage/orders/batch_cancel", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseCancelResultWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseCancelResult>>(result.Data?.Results);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseEditOrderResult>> EditOrderAsync(string orderId, decimal price, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("order_id", orderId);
            parameters.AddString("price", price);
            parameters.AddString("size", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/brokerage/orders/edit", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseEditOrderResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result;

            if (!result.Data.Success && result.Data.ErrorResponse != null)
                return result.AsError<CoinbaseEditOrderResult>(new ServerError($"{result.Data.ErrorResponse.ErrorCode}: {result.Data.ErrorResponse.Message}"));

            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/orders/historical/{orderId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseOrder>(result.Data?.Order);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseOrder>>> GetOrdersAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddOptional("order_ids", orderIds?.ToArray());
            parameters.AddOptional("product_ids", symbols?.ToArray());
            parameters.AddOptionalEnum("product_type", symbolType);
            parameters.AddOptional("order_status", orderStatus?.Select(EnumConverter.GetString).ToArray());
            parameters.AddOptional("time_in_forces", timeInForces?.Select(EnumConverter.GetString).ToArray());
            parameters.AddOptional("order_types", orderTypes?.Select(EnumConverter.GetString).ToArray());
            parameters.AddOptionalEnum("order_side", orderSide);
            parameters.AddOptional("start_date", startTime?.ToRfc3339String());
            parameters.AddOptional("end_date", endTime?.ToRfc3339String());
            parameters.AddOptional("order_placement_source", orderSource);
            parameters.AddOptionalEnum("contract_expiry_type", expiryType);
            parameters.AddOptional("asset_filters", assets?.ToArray());
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("cursor", cursor);
            parameters.AddOptionalEnum("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/brokerage/orders/historical/batch", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrdersWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseOrder>>(result.Data?.Orders);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseUserTrades>> GetUserTradesAsync(
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
            var parameters = new ParameterCollection();
            parameters.AddOptional("order_ids", orderIds?.ToArray());
            parameters.AddOptional("trade_ids", tradeIds?.ToArray());
            parameters.AddOptional("product_ids", symbols?.ToArray());
            parameters.AddOptional("start_sequence_timestamp", startTime?.ToRfc3339String());
            parameters.AddOptional("end_sequence_timestamp", endTime?.ToRfc3339String());
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("cursor", cursor);
            parameters.AddOptionalEnum("sort_by", sortBy);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/orders/historical/fills", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseUserTrades>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseOrderResult>> ClosePositionAsync(string symbol, decimal? quantity = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("product_id", symbol);
            parameters.Add("client_order_id", clientOrderId ?? ExchangeHelpers.RandomString(24));
            parameters.AddOptionalString("size", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/brokerage/orders/close_position", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseFuturesPosition>>> GetFuturesPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/cfm/positions", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesPositionsWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseFuturesPosition>>(result.Data?.Positions);
        }

        #endregion

        #region Get Futures Position

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseFuturesPosition>> GetFuturesPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/cfm/positions/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseFuturesPosition>(result.Data?.Position);
        }

        #endregion

        #region Get Perpetual Positions

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePerpetualPositions>> GetPerpetualPositionsAsync(string portfolioId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/intx/positions/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPositions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Perpetual Position

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePerpetualPosition>> GetPerpetualPositionAsync(string portfolioId, string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/intx/positions/{portfolioId}/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbasePerpetualPosition>(result.Data?.Position);
        }

        #endregion
    }
}
