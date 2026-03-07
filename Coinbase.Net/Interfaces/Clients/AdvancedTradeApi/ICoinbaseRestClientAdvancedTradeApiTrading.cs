using System.Threading.Tasks;
using System.Threading;
using System;
using CryptoExchange.Net.Objects;
using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using System.Collections.Generic;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase trading endpoints, placing and managing orders.
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_postorder" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>product_id</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="orderType">Type of the order</param>
        /// <param name="quantity">["<c>base_size</c>"] Quantity in base asset</param>
        /// <param name="quoteQuantity">["<c>quote_size</c>"] Quantity in quote asset</param>
        /// <param name="price">["<c>limit_price</c>"] Limit price</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage for the order</param>
        /// <param name="marginType">["<c>margin_type</c>"] Type for margin</param>
        /// <param name="previewId">["<c>preview_id</c>"] Id to associate this order with a preview request</param>
        /// <param name="postOnly">["<c>post_only</c>"] Post only flag</param>
        /// <param name="cancelTime">["<c>end_time</c>"] Time to cancel the order</param>
        /// <param name="stopPrice">Stop order trigger price</param>
        /// <param name="stopDirection">["<c>stop_direction</c>"] Direction of the stop trigger</param>
        /// <param name="attachedOrderTriggerPrice">["<c>stopTriggerPrice</c>"] Attached order trigger price</param>
        /// <param name="attachedOrderLimitPrice">["<c>limitPrice</c>"] Attached order limit price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrderResult>> PlaceOrderAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_cancelorders" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/orders/batch_cancel
        /// </para>
        /// </summary>
        /// <param name="orderId">Id of order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseCancelResult>> CancelOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_cancelorders" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/orders/batch_cancel
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order_ids</c>"] Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseCancelResult[]>> CancelOrdersAsync(IEnumerable<string> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Edit an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_editorder" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/orders/edit
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>order_id</c>"] Id of order to edit</param>
        /// <param name="price">["<c>price</c>"] New order price</param>
        /// <param name="quantity">["<c>size</c>"] New order quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseEditOrderResult>> EditOrderAsync(string orderId, decimal price, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get order details 
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_gethistoricalorder" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/orders/historical/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders. Note that open orders do not adhere to the time filter
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_gethistoricalorders" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/orders/historical/batch
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order_ids</c>"] Filter by order id</param>
        /// <param name="symbols">["<c>product_ids</c>"] Filter by symbol</param>
        /// <param name="symbolType">["<c>product_type</c>"] Filter by symbol type</param>
        /// <param name="orderStatus">["<c>order_status</c>"] Filter by order status</param>
        /// <param name="timeInForces">["<c>time_in_forces</c>"] Filter by time in force</param>
        /// <param name="orderTypes">["<c>order_types</c>"] Filter by order type</param>
        /// <param name="orderSide">["<c>order_side</c>"] Filter by order side</param>
        /// <param name="startTime">["<c>start_date</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_date</c>"] Filter by end time</param>
        /// <param name="orderSource">["<c>order_placement_source</c>"] Filter by source</param>
        /// <param name="expiryType">["<c>contract_expiry_type</c>"] Filter by contract expiry type</param>
        /// <param name="assets">["<c>asset_filters</c>"] Filter by assets</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="cursor">["<c>cursor</c>"] Page cursor</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrder[]>> GetOrdersAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getfills" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/orders/historical/fills
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>order_ids</c>"] Filter by order id</param>
        /// <param name="tradeIds">["<c>trade_ids</c>"] Filter by trade id</param>
        /// <param name="symbols">["<c>product_ids</c>"] Filter by symbol</param>
        /// <param name="startTime">["<c>start_sequence_timestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_sequence_timestamp</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="cursor">["<c>cursor</c>"] Page cursor</param>
        /// <param name="sortBy">["<c>sort_by</c>"] Sort type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseUserTrades>> GetUserTradesAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? tradeIds = null,
            IEnumerable<string>? symbols = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            string? cursor = null,
            TradeSortBy? sortBy = null,
            CancellationToken ct = default);

        /// <summary>
        /// Close a position
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_closeposition" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/orders/close_position
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>product_id</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id for the close order</param>
        /// <param name="quantity">["<c>size</c>"] Quantity to close</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrderResult>> ClosePositionAsync(string symbol, decimal? quantity = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get expiring futures positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getfcmpositions" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/cfm/positions
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseFuturesPosition[]>> GetFuturesPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get expiring futures position for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getfcmposition" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/cfm/positions/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseFuturesPosition>> GetFuturesPositionAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get a list of open positions in your Perpetuals portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getintxportfoliosummary" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/intx/positions/{portfolioId}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">["<c>portfolioId</c>"] Portfolio uuid</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePerpetualPositions>> GetPerpetualPositionsAsync(string portfolioId, CancellationToken ct = default);

        /// <summary>
        /// Get a specific Perpetual position
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getintxposition" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/intx/positions/{portfolioId}/{symbol}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">["<c>portfolioId</c>"] Portfolio uuid</param>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePerpetualPosition>> GetPerpetualPositionAsync(string portfolioId, string symbol, CancellationToken ct = default);

    }
}
