using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net;
using Coinbase.Net.Enums;
using System.Drawing;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    internal partial class CoinbaseRestClientAdvancedTradeApi : ICoinbaseRestClientAdvancedTradeApiShared
    {
        private const string _topicSpotId = "CoinbaseSpot";
        private const string _topicFuturesId = "CoinbaseFutures";

        public string Exchange => "Coinbase";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };
        public TradingMode[] SupportedFuturesModes => new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Asset client
        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(false);

        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var fiatAssets = ExchangeData.GetFiatAssetsAsync(ct: ct);
            var cryptoAssets = ExchangeData.GetCryptoAssetsAsync(ct: ct);
            await Task.WhenAll(fiatAssets, cryptoAssets).ConfigureAwait(false);

            if (!fiatAssets.Result)
                return fiatAssets.Result.AsExchangeResult<SharedAsset[]>(Exchange, null, default);
            if (!cryptoAssets.Result)
                return cryptoAssets.Result.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            var result = new List<SharedAsset>();
            result.AddRange(fiatAssets.Result.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name
            }));

            result.AddRange(cryptoAssets.Result.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name
            }));

            return cryptoAssets.Result.AsExchangeResult<SharedAsset[]>(Exchange, TradingMode.Spot, result.ToArray());
        }

        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var cryptoAssets = await ExchangeData.GetCryptoAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!cryptoAssets)
                return cryptoAssets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            var cryptoAsset = cryptoAssets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (cryptoAsset != null)
            {
                return cryptoAssets.AsExchangeResult(Exchange, TradingMode.Spot, new SharedAsset(cryptoAsset.Asset)
                {
                    FullName = cryptoAsset.Name
                });
            }

            var fiatAssets = await ExchangeData.GetFiatAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!fiatAssets)
                return fiatAssets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            var fiatAsset = cryptoAssets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (fiatAsset == null)
                return cryptoAssets.AsExchangeError<SharedAsset>(Exchange, new ServerError("Asset not found"));

            return fiatAssets.AsExchangeResult(Exchange, TradingMode.Spot, new SharedAsset(fiatAsset.Asset)
            {
                FullName = fiatAsset.Name
            });
        }

        #endregion

        #region Balance Client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            if (request.TradingMode == TradingMode.Spot || request.TradingMode == null)
            {
                var result = await Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedBalance[]>(Exchange, TradingMode.Spot, 
                    result.Data.Accounts.Where(x => x.Type == AccountType.Crypto || x.Type == AccountType.Fiat).Select(x => 
                    new SharedBalance(x.Asset, x.AvailableBalance.Value, x.AvailableBalance.Value + x.HoldBalance.Value)).ToArray());
            }
            else if (request.TradingMode.Value.IsPerpetual())
            {
                var portfolioId = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "PortfolioId");
                if (portfolioId == default)
                    return new ExchangeWebResult<SharedBalance[]>(Exchange, new ArgumentError("PortfolioId is required as Exchange parameter for retrieving Perpetual futures balances"));

                var result = await Account.GetPerpetualBalancesAsync(portfolioId, ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedBalance[]>(Exchange, request.TradingMode.Value, result.Data.Balances.Select(x => new SharedBalance(x.Asset.AssetId, x.MaxWithdrawQuantity, x.Quantity)).ToArray());
            }
            else
            {
                // Delivery futures
                var result = await Account.GetFuturesBalanceSummaryAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedBalance[]>(Exchange, request.TradingMode.Value, new[] { new SharedBalance(result.Data.CfmUsdBalance.Asset, result.Data.CfmUsdBalance.Value, result.Data.TotalUsdBalance.Value) });
            }
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, new ArgumentError("AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var depositAddresses = await Account.GetDepositAddressesAsync(accountId, ct: ct).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, TradingMode.Spot, depositAddresses.Data.Data.Select(x => new SharedDepositAddress(x.Network, x.Address)
            {
                Network = x.Network
            }).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(SharedPaginationSupport.Descending, false, 100)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, new ArgumentError("AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            // Determine page token
            string? toId = null;
            if (pageToken is FromIdToken fromToken)
                toId = fromToken.FromToken;

            // Get data
            var deposits = await Account.GetDepositsAsync(
                accountId,
                order: Enums.SortOrder.Descending,
                toId: toId,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            // Determine next token
            FromIdToken? nextToken = null;
            if (deposits.Data.Pagination.NextUri != null)
                nextToken = new FromIdToken(deposits.Data.Data.OrderBy(x => x.CreateTime).First().Id);

            return deposits.AsExchangeResult<SharedDeposit[]>(Exchange, TradingMode.Spot, deposits.Data.Data.Select(x => new SharedDeposit(x.Quantity.Asset, x.Quantity.Value, x.Status == Enums.WithdrawalStatus.Completed, x.CreateTime)
            {
                Id = x.Id
            }).ToArray(), nextToken);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 5000, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trades client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);

        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            // Get data
            var result = await ExchangeData.GetTradeHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            // Return
            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol!.TradingMode, result.Data.Trades.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }
        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(SharedPaginationSupport.Descending, true, 1000, false);

        async Task<ExchangeWebResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ITradeHistoryRestClient)this).GetTradeHistoryOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var limit = request.Limit ?? 1000;
            var result = await ExchangeData.GetTradeHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            DateTimeToken? nextToken = null;
            if (result.Data.Trades.Any() && result.Data.Trades.Count() == limit)
                nextToken = new DateTimeToken(result.Data.Trades.Min(x => x.Timestamp.AddMilliseconds(-1)));

            // Return
            return result.AsExchangeResult<SharedTrade[]>(Exchange, request.Symbol!.TradingMode, result.Data.Trades.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray(), nextToken);
        }
        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(SharedPaginationSupport.Descending, false, 100)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, new ArgumentError("AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            // Determine page token
            string? toId = null;
            if (pageToken is FromIdToken fromToken)
                toId = fromToken.FromToken;

            // Get data
            var deposits = await Account.GetWithdrawalsAsync(
                accountId,
                order: Enums.SortOrder.Descending,
                toId: toId,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            // Determine next token
            FromIdToken? nextToken = null;
            if (deposits.Data.Pagination.NextUri != null)
                nextToken = new FromIdToken(deposits.Data.Data.OrderBy(x => x.CreateTime).First().Id);

            return deposits.AsExchangeResult<SharedWithdrawal[]>(Exchange, TradingMode.Spot, deposits.Data.Data.Select(x => new SharedWithdrawal(x.Quantity.Asset, string.Empty, x.Quantity.Value, x.Status == Enums.WithdrawalStatus.Completed, x.CreateTime)
            {
                Id = x.Id,
                Fee = x.Fee.Value
            }).ToArray(), nextToken);
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions()
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to withdraw from", "123123")
            }
        };
        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            // Get data
            var withdrawal = await Account.WithdrawCryptoAsync(
                accountId,
                request.Address,
                request.Quantity,
                request.Asset,
                destinationTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.Id));
        }

        #endregion

        #region Spot Symbol client
        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);

        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(Enums.SymbolType.Spot, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            // Coinbase return duplicate spot symbols for some pairs
            // For example both BTC-USD and BTC-USDC is returned, referring to the same symbol
            // Also, when for example subscribing to BTC-USDC in update the name is BTC-USD instead
            // The library uses the BTC-USDC notation
            var symbolData = result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.SymbolStatus == SymbolStatus.Online && !s.IsDisabled && !s.TradingDisabled)
            {
                MinTradeQuantity = s.MinOrderQuantity,
                MaxTradeQuantity = s.MaxOrderQuantity,
                QuantityStep = s.QuantityStep,
                PriceStep = s.PriceStep
            }).ToArray();

            var originalSymbols = symbolData.Where(x => x.QuoteAsset != "USD").ToArray();
            foreach (var item in symbolData.Where(x => x.QuoteAsset == "USD"))            
                item.QuoteAsset = "USDC";            

            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, symbolData);
            return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, TradingMode.Spot, originalSymbols);
        }

        #endregion

        #region Spot Ticker client

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, result.Data.Symbol), result.Data.Symbol, result.Data.LastPrice, null, null, result.Data.Volume24h ?? 0, result.Data.PricePercentageChange24h)
            {
                QuoteVolume = result.Data.ApproximateQuote24hVolume
            });
        }

        EndpointOptions<GetTickersRequest> ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            var originalSymbols = result.Data.Where(x => x.QuoteAsset != "USD").ToArray();
            return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, TradingMode.Spot, originalSymbols.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol), x.Symbol, x.LastPrice, null, null, x.Volume24h ?? 0, x.PricePercentageChange24h)
            {
                QuoteVolume = x.ApproximateQuote24hVolume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(true);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetBookTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(request.Symbol!.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, resultTicker.Data.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice,
                resultTicker.Data.BestAskQuantity,
                resultTicker.Data.BestBidPrice,
                resultTicker.Data.BestBidQuantity));
        }

        #endregion

        #region Spot Order Client

        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAsset);

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol!.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInBaseAsset,
                quoteQuantity: request.Quantity?.QuantityInQuoteAsset,
                price: request.Price,
                clientOrderId: request.ClientOrderId,
                postOnly: request.OrderType == SharedOrderType.LimitMaker ? true: null,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : order.Data.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderStatus),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.OrderConfiguration.Quantity, order.Data.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                IsTriggerOrder = order.Data.OrderType == OrderType.Stop || order.Data.OrderType == OrderType.StopLimit
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Open],
                symbolType: SymbolType.Spot,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderStatus),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice == 0 ? null : x.AverageFillPrice,
                OrderPrice = x.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(x.OrderConfiguration.Quantity, x.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.LastFillTime,
                IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 1000, true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            // Determine page token
            DateTime? fromTime = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTime = dateTimeToken.LastTime;

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Canceled, OrderStatus.Filled],
                symbolType: SymbolType.Spot,
                startTime: request.StartTime,
                endTime: fromTime ?? request.EndTime,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Any())
                nextToken = new DateTimeToken(orders.Data.Min(o => o.CreateTime).AddMilliseconds(-1));

            return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderStatus),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice == 0 ? null : x.AverageFillPrice,
                OrderPrice = x.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(x.OrderConfiguration.Quantity, x.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.LastFillTime,
                IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            var orders = await Trading.GetUserTradesAsync(orderIds: [request.OrderId], ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var orders = await Trading.GetUserTradesAsync(
                symbols : [request.Symbol!.GetSymbol(FormatSymbol)],
                startTime: request.StartTime,
                endTime: fromTimestamp ?? request.EndTime,
                limit: request.Limit ?? 100,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Trades.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(orders.Data.Trades.Min(o => o.Timestamp).AddMilliseconds(-1));

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, TradingMode.Spot, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(order.Data.OrderId!));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Pending || status == OrderStatus.Open || status == OrderStatus.Queued) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.Expired || status == OrderStatus.Failed) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce tif)
        {
            if (tif == TimeInForce.GoodTillCanceled) return SharedTimeInForce.GoodTillCanceled;
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        private NewOrderType GetOrderType(SharedOrderType orderType, SharedTimeInForce? timeInForce)
        {
            if (orderType == SharedOrderType.LimitMaker || orderType == SharedOrderType.Limit)
            {
                if (!timeInForce.HasValue || timeInForce == SharedTimeInForce.GoodTillCanceled)
                    return NewOrderType.Limit;

                if (timeInForce == SharedTimeInForce.FillOrKill)
                    return NewOrderType.LimitFillOrKill;

                return NewOrderType.LimitImmediateOrCancel;
            }

            return NewOrderType.Market;
        }

        #endregion

        #region Futures Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedFuturesTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol!.TradingMode, 
                new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, resultTicker.Data.Symbol), resultTicker.Data.Symbol, resultTicker.Data.LastPrice, null, null, resultTicker.Data.Volume24h ?? 0, resultTicker.Data.PricePercentageChange24h)
            {
                FundingRate = resultTicker.Data.FutureProductDetails!.PerpetualDetails!.FundingRate,
                NextFundingTime = resultTicker.Data.FutureProductDetails.PerpetualDetails.FundingTime
            });
        }

        EndpointOptions<GetTickersRequest> IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker[]>(Exchange, validationError);

            var expiringTime = request.TradingMode == null || request.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var resultTicker = await ExchangeData.GetSymbolsAsync(SymbolType.Futures, expiryType: expiringTime, ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedFuturesTicker[]>(Exchange, null, default);

            var data = resultTicker.Data;
            return resultTicker.AsExchangeResult<SharedFuturesTicker[]>(Exchange, request.TradingMode.HasValue ? [request.TradingMode.Value]: SupportedTradingModes,
                data.Select(x => 
                    new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, x.LastPrice, null, null, x.Volume24h ?? 0, x.PricePercentageChange24h)
                    {
                        FundingRate = x.FutureProductDetails!.PerpetualDetails?.FundingRate,
                        NextFundingTime = x.FutureProductDetails.PerpetualDetails?.FundingTime
                    }).ToArray());
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesSymbol[]>(Exchange, validationError);

            var expiringTime = request.TradingMode == null || request.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var resultTicker = await ExchangeData.GetSymbolsAsync(SymbolType.Futures, expiryType: expiringTime, ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, null, default);

            var data = resultTicker.Data;

            var response = resultTicker.AsExchangeResult<SharedFuturesSymbol[]>(Exchange, request.TradingMode.HasValue ? [request.TradingMode.Value] : SupportedTradingModes,
                data.Select(x =>
                    new SharedFuturesSymbol(
                        x.FutureProductDetails!.ContractExpiry == null ? TradingMode.PerpetualLinear: TradingMode.DeliveryLinear,
                        x.FutureProductDetails.ContractCode,
                        x.QuoteAsset,
                        x.Symbol,
                        x.SymbolStatus == SymbolStatus.Online && !x.IsDisabled && !x.TradingDisabled)
                    {
                        MinTradeQuantity = x.MinOrderQuantity,
                        MaxTradeQuantity = x.MaxOrderQuantity,
                        QuantityStep = x.QuantityStep,
                        PriceStep = x.PriceStep,
                        ContractSize = x.FutureProductDetails.ContractSize,
                        DeliveryTime = x.FutureProductDetails.ContractExpiry,
                        MaxLongLeverage = x.FutureProductDetails.PerpetualDetails?.MaxLeverage,
                        MaxShortLeverage = x.FutureProductDetails.PerpetualDetails?.MaxLeverage
                    }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicFuturesId, response.Data);
            return response;
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedOpenInterest(result.Data.FutureProductDetails!.PerpetualDetails?.OpenInterest ?? 0));
        }

        #endregion

        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;

        SharedOrderType[] IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        SharedTimeInForce[] IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts,
                SharedQuantityType.Contracts);

        string IFuturesOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(32);

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(false);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol!.TradingMode,
                SupportedFuturesModes,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderTypes,
                ((IFuturesOrderRestClient)this).FuturesSupportedTimeInForce,
                ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (request.ReduceOnly == true)
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError($"ReduceOnly flag is not available on {Exchange}, use ClosePositionAsync with quantity to reduce a position"));

            var result = await Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity?.QuantityInContracts,
                price: request.Price,
                leverage: request.Leverage,
                marginType: request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Cross ? MarginType.Cross : MarginType.Isolated,
                postOnly: request.OrderType == SharedOrderType.LimitMaker,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.SuccessResponse.OrderId.ToString()));
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.OrderType == OrderType.Limit ? SharedOrderType.Limit : order.Data.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                order.Data.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.OrderStatus),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.OrderConfiguration.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: order.Data.QuoteQuantityFilled, contractQuantity: order.Data.QuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                Leverage = order.Data.Leverage,
                TriggerPrice = order.Data.OrderConfiguration.StopPrice,
                IsTriggerOrder = order.Data.OrderType == OrderType.Stop || order.Data.OrderType == OrderType.StopLimit
            });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var expiryType = ((request.Symbol?.TradingMode ?? request.TradingMode) ?? TradingMode.PerpetualLinear) == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var orders = await Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Open],
                symbolType: SymbolType.Futures,
                expiryType: expiryType,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, SupportedFuturesModes, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderStatus),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice == 0 ? null : x.AverageFillPrice,
                OrderPrice = x.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.OrderConfiguration.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.QuoteQuantityFilled, contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.LastFillTime,
                Leverage = x.Leverage,
                TriggerPrice = x.OrderConfiguration.StopPrice,
                IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder[]>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var expiryType = request.Symbol!.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var orders = await Trading.GetOrdersAsync(
                symbols: [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Canceled, OrderStatus.Filled],
                symbolType: SymbolType.Futures,
                expiryType: expiryType,
                startTime: request.StartTime,
                endTime: fromTimestamp ?? request.EndTime,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new DateTimeToken(orders.Data.Min(o => o.CreateTime).AddMilliseconds(-1));

            return orders.AsExchangeResult<SharedFuturesOrder[]>(Exchange, request.Symbol!.TradingMode, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId.ToString(),
                x.OrderType == OrderType.Limit ? SharedOrderType.Limit : x.OrderType == OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.OrderStatus),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice == 0 ? null : x.AverageFillPrice,
                OrderPrice = x.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: x.OrderConfiguration.Quantity),
                QuantityFilled = new SharedOrderQuantity(quoteAssetQuantity: x.QuoteQuantityFilled, contractQuantity: x.QuantityFilled),
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.LastFillTime,
                Leverage = x.Leverage,
                TriggerPrice = x.OrderConfiguration.StopPrice,
                IsTriggerOrder = x.OrderType == OrderType.Stop || x.OrderType == OrderType.StopLimit
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            var orders = await Trading.GetUserTradesAsync(orderIds: [request.OrderId], ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol!.TradingMode, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true, 100, true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var orders = await Trading.GetUserTradesAsync(
                symbols: [request.Symbol!.GetSymbol(FormatSymbol)],
                startTime: request.StartTime,
                endTime: fromTimestamp ?? request.EndTime,
                limit: request.Limit ?? 100,
                ct: ct
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Trades.Count() == (request.Limit ?? 100))
                nextToken = new DateTimeToken(orders.Data.Trades.Min(o => o.Timestamp).AddMilliseconds(-1));

            return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, request.Symbol!.TradingMode, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), 
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(order.Data.OrderId!));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true);
        async Task<ExchangeWebResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPosition[]>(Exchange, validationError);

            var tradingMode = request.Symbol?.TradingMode ?? request.TradingMode;
            if (tradingMode == null || request.TradingMode == TradingMode.PerpetualLinear)
            {
                var portfolioId = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "PortfolioId");
                if (portfolioId == default)
                    return new ExchangeWebResult<SharedPosition[]>(Exchange, new ArgumentError("PortfolioId is required as Exchange parameter for retrieving Perpetual futures balances"));

                var result = await Trading.GetPerpetualPositionsAsync(portfolioId, ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedPosition[]>(Exchange, TradingMode.PerpetualLinear, result.Data.Positions.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, Math.Abs(x.NetQuantity), null)
                {
                    UnrealizedPnl = x.UnrealizedPnl.Value,
                    LiquidationPrice = x.LiquidationPrice.Value == 0 ? null : x.LiquidationPrice.Value,
                    Leverage = x.Leverage,
                    AverageOpenPrice = x.EntryVolumeWeightedAveragePrice.Value,
                    PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
            else
            {
                var result = await Trading.GetFuturesPositionsAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedPosition[]>(Exchange, null, default);

                return result.AsExchangeResult<SharedPosition[]>(Exchange, TradingMode.DeliveryLinear, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, x.Symbol), x.Symbol, Math.Abs(x.NumberOfContracts), null)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageOpenPrice = x.AverageEntryPrice,
                    PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedFuturesModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await Trading.ClosePositionAsync(
                symbol,
                quantity: request.Quantity,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        #endregion

        #region Klines Client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, true, 350, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.SixHours,
            SharedKlineInterval.OneDay);

        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 350;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * limit;
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                interval,
                startTime,
                endTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return new ExchangeWebResult<SharedKline[]>(Exchange, request.Symbol!.TradingMode, result.As<SharedKline[]>(default));

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                var minOpenTime = result.Data.Min(x => x.OpenTime);
                if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
            }

            return result.AsExchangeResult<SharedKline[]>(Exchange, request.Symbol!.TradingMode, result.Data.Reverse().Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextToken);
        }

        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var symbolType = request.Symbol!.TradingMode == TradingMode.Spot ? SymbolType.Spot : SymbolType.Futures;
            var result = await Account.GetFeeInfoAsync(symbolType, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, symbolType == SymbolType.Spot ? [TradingMode.Spot] : SupportedFuturesModes, new SharedFee(result.Data.FeeTier.MakerFeeRate * 100, result.Data.FeeTier.TakerFeeRate * 100));
        }
        #endregion

        #region Spot Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(true);

        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).PlaceSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes, ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(                
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                NewOrderType.StopLimit,
                quantity: request.Quantity?.QuantityInBaseAsset,
                quoteQuantity: request.Quantity?.QuantityInQuoteAsset,
                // Simulate market order by adding/removing 10% to the trigger price as order price
                price: request.OrderPrice ?? (request.TriggerPrice + (request.TriggerPrice * 0.1m * (request.OrderSide == SharedOrderSide.Buy ? 1 : -1))),
                stopPrice: request.TriggerPrice,
                clientOrderId: request.ClientOrderId,
                stopDirection: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopDirection.Up : StopDirection.Down,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        EndpointOptions<GetOrderRequest> ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        async Task<ExchangeWebResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).GetSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotTriggerOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                SharedOrderType.Limit,
                order.Data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.OrderStatus),
                order.Data.OrderConfiguration.StopPrice ?? 0,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(order.Data.OrderConfiguration.Quantity, order.Data.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(OrderStatus orderStatus)
        {
            if (orderStatus == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (orderStatus == OrderStatus.Canceled
                || orderStatus == OrderStatus.Expired
                || orderStatus == OrderStatus.Failed)
            {
                return SharedTriggerOrderStatus.CanceledOrRejected;
            }

            return SharedTriggerOrderStatus.Active;
        }

        EndpointOptions<CancelOrderRequest> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).CancelSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(request.OrderId));
        }

        #endregion

        #region Futures Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(true)
        {
        };

        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = ((IFuturesTriggerOrderRestClient)this).PlaceFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes, side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, ((IFuturesOrderRestClient)this).FuturesSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side,
                NewOrderType.StopLimit,
                quantity: request.Quantity.QuantityInContracts,
                // Simulate market order by adding/removing 10% to the trigger price as order price
                price: request.OrderPrice ?? (request.TriggerPrice + (request.TriggerPrice * 0.1m * (request.OrderDirection == SharedTriggerOrderDirection.Enter ? 1 : -1))),
                stopPrice: request.TriggerPrice,
                stopDirection: request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? StopDirection.Up : StopDirection.Down,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        private OrderSide GetTriggerOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
            {
                if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
                    return OrderSide.Buy;
                return OrderSide.Sell;
            }

            if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
                return OrderSide.Sell;
            return OrderSide.Buy;
        }

        EndpointOptions<GetOrderRequest> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        async Task<ExchangeWebResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).GetFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesTriggerOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                SharedOrderType.Limit,
                order.Data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit,
                ParseTriggerOrderStatus(order.Data.OrderStatus),
                order.Data.OrderConfiguration.StopPrice ?? 0,
                null,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderId,
                AveragePrice = order.Data.AverageFillPrice == 0 ? null : order.Data.AverageFillPrice,
                OrderPrice = order.Data.OrderConfiguration.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.OrderConfiguration.Quantity, quoteAssetQuantity: order.Data.OrderConfiguration.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: order.Data.QuantityFilled, quoteAssetQuantity: order.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.LastFillTime,
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        EndpointOptions<CancelOrderRequest> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((IFuturesTriggerOrderRestClient)this).CancelFuturesTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedId(request.OrderId));
        }

        #endregion

        private async Task<string?> GetAccountIdAsync(ExchangeParameters? parameters, string? asset, CancellationToken ct)
        {
            var accountId = ExchangeParameters.GetValue<string>(parameters, Exchange, "AccountId");
            if (accountId != default)
                return accountId;

            if (asset == null)
                return null;

            var accounts = await Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
            if (!accounts)
                return null;

            var account = accounts.Data.Accounts.FirstOrDefault(x => x.Asset == asset);
            if (account == null)
                return null;

            return account.AccountId;
        }
    }
}
