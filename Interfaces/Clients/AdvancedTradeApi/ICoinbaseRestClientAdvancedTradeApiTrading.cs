using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System;
using CryptoExchange.Net.Objects;
using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using System.Collections;
using System.Collections.Generic;

namespace Coinbase.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Coinbase trading endpoints, placing and managing orders.
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_postorder" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="orderType">Type of the order</param>
        /// <param name="quantity">Quantity in base asset</param>
        /// <param name="quoteQuantity">Quantity in quote asset</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="leverage">Leverage for the order</param>
        /// <param name="marginType">Type for margin</param>
        /// <param name="previewId">Id to associate this order with a preview request</param>
        /// <param name="postOnly">Post only flag</param>
        /// <param name="cancelTime">Time to cancel the order</param>
        /// <param name="stopPrice">Stop order trigger price</param>
        /// <param name="stopDirection">Direction of the stop trigger</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrderResult>> PlaceOrderAsync(string symbol, OrderSide side, NewOrderType orderType, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, decimal? leverage = null, MarginType? marginType = null, string? previewId = null, bool? postOnly = null, DateTime? cancelTime = null, decimal? stopPrice = null, StopDirection? stopDirection = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_cancelorders" /></para>
        /// </summary>
        /// <param name="orderId">Id of order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseCancelResult>> CancelOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel orders
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_cancelorders" /></para>
        /// </summary>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbaseCancelResult>>> CancelOrdersAsync(IEnumerable<string> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Edit an order
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_editorder" /></para>
        /// </summary>
        /// <param name="orderId">Id of order to edit</param>
        /// <param name="price">New order price</param>
        /// <param name="quantity">New order quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseEditOrderResult>> EditOrderAsync(string orderId, decimal price, decimal quantity, CancellationToken ct = default);
        
        /// <summary>
        /// Get order details 
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_gethistoricalorder" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrder>> GetOrderAsync(string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders. Note that open orders do not adhere to the time filter
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_gethistoricalorders" /></para>
        /// </summary>
        /// <param name="orderIds">Filter by order id</param>
        /// <param name="symbols">Filter by symbol</param>
        /// <param name="symbolType">Filter by symbol type</param>
        /// <param name="orderStatus">Filter by order status</param>
        /// <param name="timeInForces">Filter by time in force</param>
        /// <param name="orderTypes">Filter by order type</param>
        /// <param name="orderSide">Filter by order side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="orderSource">Filter by source</param>
        /// <param name="expiryType">Filter by contract expiry type</param>
        /// <param name="assets">Filter by assets</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="cursor">Page cursor</param>
        /// <param name="sortBy">Sort order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbaseOrder>>> GetOrdersAsync(
            IEnumerable<string>? orderIds = null,
            IEnumerable<string>? symbols = null,
            IEnumerable<SymbolType>? symbolType = null,
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
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getfills" /></para>
        /// </summary>
        /// <param name="orderIds">Filter by order id</param>
        /// <param name="tradeIds">Filter by trade id</param>
        /// <param name="symbols">Filter by symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="cursor">Page cursor</param>
        /// <param name="sortBy">Sort type</param>
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
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_closeposition" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="clientOrderId">Client order id for the close order</param>
        /// <param name="quantity">Quantity to close</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrderResult>> ClosePositionAsync(string symbol, decimal quantity, string? clientOrderId = null, CancellationToken ct = default);

    }
}
