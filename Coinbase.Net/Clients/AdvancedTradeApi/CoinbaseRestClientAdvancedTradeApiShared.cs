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
using CryptoExchange.Net.Objects.Errors;
using Coinbase.Net.Objects.Models;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    internal partial class CoinbaseRestClientAdvancedTradeApi : ICoinbaseRestClientAdvancedTradeApiShared
    {
        private const string _topicSpotId = "CoinbaseSpot";
        private const string _topicFuturesId = "CoinbaseFutures";
        private const string _exchangeName = "Coinbase";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };
        public TradingMode[] SupportedFuturesModes => new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(_exchangeName, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(CoinbaseExchange.Metadata, this);

        #region Asset client
        GetAssetsOptions IAssetsRestClient.GetAssetsOptions { get; } = new GetAssetsOptions(_exchangeName, false);

        async Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var fiatAssets = ExchangeData.GetFiatAssetsAsync(ct: ct);
            var cryptoAssets = ExchangeData.GetCryptoAssetsAsync(ct: ct);
            await Task.WhenAll(fiatAssets, cryptoAssets).ConfigureAwait(false);

            if (!fiatAssets.Result.Success)
                return HttpResult.Fail<SharedAsset[]>(fiatAssets.Result);
            if (!cryptoAssets.Result.Success)
                return HttpResult.Fail<SharedAsset[]>(cryptoAssets.Result);

            var result = new List<SharedAsset>();
            result.AddRange(fiatAssets.Result.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name
            }));

            result.AddRange(cryptoAssets.Result.Data.Select(x => new SharedAsset(x.Asset)
            {
                FullName = x.Name
            }));

            return HttpResult.Ok(cryptoAssets.Result, result.ToArray());
        }

        GetAssetOptions IAssetsRestClient.GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        async Task<HttpResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var cryptoAssets = await ExchangeData.GetCryptoAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!cryptoAssets.Success)
                return HttpResult.Fail<SharedAsset>(cryptoAssets);

            var cryptoAsset = cryptoAssets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (cryptoAsset != null)
            {
                return HttpResult.Ok(cryptoAssets, new SharedAsset(cryptoAsset.Asset)
                {
                    FullName = cryptoAsset.Name
                });
            }

            var fiatAssets = await ExchangeData.GetFiatAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!fiatAssets.Success)
                return HttpResult.Fail<SharedAsset>(fiatAssets);

            var fiatAsset = cryptoAssets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (fiatAsset == null)
                return HttpResult.Fail<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(fiatAssets, new SharedAsset(fiatAsset.Asset)
            {
                FullName = fiatAsset.Name
            });
        }

        #endregion

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Spot, AccountTypeFilter.Futures);

        async Task<HttpResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Spot || request.AccountType == null)
            {
                var result = await Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Accounts.Where(x => x.Type == AccountType.Crypto || x.Type == AccountType.Fiat).Select(x => 
                    new SharedBalance(x.Asset, x.AvailableBalance.Value, x.AvailableBalance.Value + x.HoldBalance.Value)).ToArray());
            }
            else if (request.AccountType == SharedAccountType.PerpetualLinearFutures || request.AccountType == SharedAccountType.PerpetualInverseFutures)
            {
                var portfolioId = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "PortfolioId");
                if (portfolioId == default)
                    return HttpResult.Fail<SharedBalance[]>(Exchange, ArgumentError.Missing("PortfolioId", "PortfolioId is required as Exchange parameter for retrieving Perpetual futures balances"));

                var result = await Account.GetPerpetualBalancesAsync(portfolioId, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Balances.Select(x => new SharedBalance(x.Asset.AssetId, x.MaxWithdrawQuantity, x.Quantity)).ToArray());
            }
            else
            {
                // Delivery futures
                var result = await Account.GetFuturesBalanceSummaryAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, new[] { new SharedBalance(result.Data.CfmUsdBalance.Asset, result.Data.CfmUsdBalance.Value, result.Data.TotalUsdBalance.Value) });
            }
        }

        #endregion

        #region Deposit client

        GetDepositAddressesOptions IDepositRestClient.GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        async Task<HttpResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var depositAddresses = await Account.GetDepositAddressesAsync(accountId, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, depositAddresses.Data.Data.Select(x => new SharedDepositAddress(x.Network, x.Address)
            {
                Network = x.Network
            }).ToArray());
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(_exchangeName, true, true, false, 100)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        async Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetDepositsAsync(
                accountId,
                order: direction == DataDirection.Descending ? SortOrder.Descending : SortOrder.Ascending,
                fromId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toId: direction == DataDirection.Descending ? pageParams.FromId : null,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => direction == DataDirection.Ascending 
                    ? Pagination.NextPageFromId(result.Data.Data.OrderByDescending(x => x.CreateTime).First().Id)
                    : Pagination.NextPageFromId(result.Data.Data.OrderBy(x => x.CreateTime).First().Id),
                result.Data.Data.Length,
                result.Data.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x =>  
                            new SharedDeposit(
                                x.Quantity.Asset,
                                x.Quantity.Value,
                                x.Status == Enums.WithdrawalStatus.Completed,
                                x.CreateTime,
                                ParseTransferStatus(x.Status))
                            {
                                Id = x.Id
                            })
                       .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(WithdrawalStatus status)
        {
            if (status == WithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;
            if (status == WithdrawalStatus.Canceled)
                return SharedTransferStatus.Failed;
            if (status == WithdrawalStatus.Created)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 1, 5000, false);
        async Task<HttpResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Recent Trades client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 1000, false);

        async Task<HttpResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradeHistoryAsync(
                symbol,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            // Return
            return HttpResult.Ok(result, result.Data.Trades.Select(x => 
            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
            {
                Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
            }).ToArray());
        }
        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(_exchangeName, false, true, true, 1000, false);

        async Task<HttpResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetTradeHistoryAsync(
                symbol,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                // Filtering is done on second level, so need to adjust to previous second instead of millisecond
                () => Pagination.NextPageFromTime(pageParams, result.Data.Trades.Min(x => x.Timestamp).Add(TimeSpan.FromMilliseconds(-999))),
                result.Data.Trades.Length,
                result.Data.Trades.Select(x => x.Timestamp),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                       .Select(x =>  
                            new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.Timestamp)
                            {
                                Side = x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell
                            })
                       .ToArray(), nextPageRequest);
        }
        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(_exchangeName, true, true, false, 100)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to get info for", "123123")
            }
        };
        async Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetWithdrawalsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            var direction = request.Direction ?? DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Account.GetWithdrawalsAsync(
                accountId,
                order: direction == DataDirection.Descending ? SortOrder.Descending : SortOrder.Ascending,
                fromId: direction == DataDirection.Ascending ? pageParams.FromId : null,
                toId: direction == DataDirection.Descending ? pageParams.FromId : null,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                 () => direction == DataDirection.Ascending
                     ? Pagination.NextPageFromId(result.Data.Data.OrderByDescending(x => x.CreateTime).First().Id)
                     : Pagination.NextPageFromId(result.Data.Data.OrderBy(x => x.CreateTime).First().Id),
                 result.Data.Data.Length,
                 result.Data.Data.Select(x => x.CreateTime),
                 request.StartTime,
                 request.EndTime ?? DateTime.UtcNow,
                 pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedWithdrawal(
                                x.Quantity.Asset, 
                                string.Empty,
                                x.Quantity.Value, 
                                x.Status == Enums.WithdrawalStatus.Completed, 
                                x.CreateTime,
                                GetWithdrawalStatus(x))
                            {
                                Id = x.Id,
                                Fee = x.Fee.Value
                            })
                       .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(CoinbaseWithdrawal x)
        {
            if (x.Status == WithdrawalStatus.Canceled)
                return SharedTransferStatus.Failed;

            if (x.Status == WithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawalStatus.Created)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("AccountId", typeof(string), "Id of the account to withdraw from", "123123")
            }
        };
        async Task<HttpResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var accountId = await GetAccountIdAsync(request.ExchangeParameters, request.Asset, ct).ConfigureAwait(false);
            if (accountId == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Missing("AccountId", "AccountId not provided and could not be determined. Please provide the AccountId parameter in the ExchangeParameters"));

            // Get data
            var withdrawal = await Account.WithdrawCryptoAsync(
                accountId,
                request.Address,
                request.Quantity,
                request.Asset,
                destinationTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.Id));
        }

        #endregion

        #region Spot Symbol client
        GetSpotSymbolsOptions ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);

        async Task<HttpResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(Enums.SymbolType.Spot, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);

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

            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, EnvironmentName, null, symbolData);
            return HttpResult.Ok(result, originalSymbols);
        }

        async Task<ExchangeCallResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId, EnvironmentName, null))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicSpotId, EnvironmentName, null, baseAsset));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicSpotId, EnvironmentName, null))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, EnvironmentName, null, symbol));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId, EnvironmentName, null))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, EnvironmentName, null, symbolName));
        }
        #endregion

        #region Spot Ticker client

        GetSpotTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, result.Data.Symbol), result.Data.Symbol, result.Data.LastPrice, null, null, result.Data.Volume24h ?? 0, result.Data.PricePercentageChange24h)
            {
                QuoteVolume = result.Data.ApproximateQuote24hVolume
            });
        }

        GetSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetSpotTickersOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            var originalSymbols = result.Data.Where(x => x.QuoteAsset != "USD").ToArray();
            return HttpResult.Ok(result, originalSymbols.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, x.Symbol), x.Symbol, x.LastPrice, null, null, x.Volume24h ?? 0, x.PricePercentageChange24h)
            {
                QuoteVolume = x.ApproximateQuote24hVolume
            }).ToArray());
        }

        #endregion

        #region Book Ticker client

        GetBookTickerOptions IBookTickerRestClient.GetBookTickerOptions { get; } = new GetBookTickerOptions(_exchangeName, true);
        async Task<HttpResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetBookTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(request.Symbol!.TradingMode == TradingMode.Spot ? _topicSpotId : _topicFuturesId, EnvironmentName, null, resultTicker.Data.Symbol),
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

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        GetSpotOrderOptions ISpotOrderRestClient.GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, order.Data.Symbol),
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

        GetOpenSpotOrdersOptions ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new GetOpenSpotOrdersOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Open],
                symbolType: SymbolType.Spot,
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, x.Symbol),
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

        GetSpotClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, false, true, true, 1000);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var result = await Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Canceled, OrderStatus.Filled],
                symbolType: SymbolType.Spot,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                result.Data.Length,
                result.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedSpotOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, x.Symbol),
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
                            })
                       .ToArray(), nextPageRequest);
        }

        GetSpotOrderTradesOptions ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new GetSpotOrderTradesOptions(_exchangeName, true);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var orders = await Trading.GetUserTradesAsync(orderIds: [request.OrderId], ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, x.Symbol),
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.QuantityInQuoteAsset ? (x.Quantity / x.Price) : x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Fee = x.Fee,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        GetSpotUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetSpotUserTradesOptions(_exchangeName, false, true, true, 100);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotUserTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetUserTradesAsync(
                symbols : [request.Symbol!.GetSymbol(FormatSymbol)],
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                cursor: pageParams.Cursor,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => result.Data.Cursor == null ? null : Pagination.NextPageFromCursor(result.Data.Cursor),
                result.Data.Trades.Length,
                result.Data.Trades.Select(x => x.Timestamp),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, x.Symbol),
                                x.Symbol,
                                x.OrderId,
                                x.TradeId,
                                x.OrderSide == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                x.QuantityInQuoteAsset ? (x.Quantity / x.Price) : x.Quantity,
                                x.Price,
                                x.Timestamp)
                            {
                                Fee = x.Fee,
                                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
                            })
                       .ToArray(), nextPageRequest);
        }

        CancelSpotOrderOptions ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new CancelSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId!));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Pending || status == OrderStatus.Open || status == OrderStatus.Queued || status == OrderStatus.CancelQueued) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.Expired || status == OrderStatus.Failed) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Filled) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
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

        GetFuturesTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        async Task<HttpResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, resultTicker.Data.Symbol), resultTicker.Data.Symbol, resultTicker.Data.LastPrice, null, null, resultTicker.Data.Volume24h ?? 0, resultTicker.Data.PricePercentageChange24h)
            {
                FundingRate = resultTicker.Data.FutureProductDetails!.PerpetualDetails!.FundingRate,
                NextFundingTime = resultTicker.Data.FutureProductDetails.PerpetualDetails.FundingTime
            });
        }

        GetFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new GetFuturesTickersOptions(_exchangeName);
        async Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var expiringTime = request.TradingMode == null || request.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var resultTicker = await ExchangeData.GetSymbolsAsync(SymbolType.Futures, expiryType: expiringTime, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultTicker);

            var data = resultTicker.Data;
            return HttpResult.Ok(resultTicker, data.Select(x => 
                    new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), x.Symbol, x.LastPrice, null, null, x.Volume24h ?? 0, x.PricePercentageChange24h)
                    {
                        FundingRate = x.FutureProductDetails!.PerpetualDetails?.FundingRate,
                        NextFundingTime = x.FutureProductDetails.PerpetualDetails?.FundingTime
                    }).ToArray());
        }

        #endregion

        #region Futures Symbol client

        GetFuturesSymbolsOptions IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        async Task<HttpResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var expiringTime = request.TradingMode == null || request.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var resultTicker = await ExchangeData.GetSymbolsAsync(SymbolType.Futures, expiryType: expiringTime, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(resultTicker);

            var data = resultTicker.Data;

            var response = HttpResult.Ok(resultTicker,
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

            ExchangeSymbolCache.UpdateSymbolInfo(_topicFuturesId, EnvironmentName, null, response.Data!);
            return response;
        }

        async Task<ExchangeCallResult<SharedSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, EnvironmentName, null))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicFuturesId, EnvironmentName, null, baseAsset));
        }

        async Task<ExchangeCallResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, EnvironmentName, null))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, EnvironmentName, null, symbol));
        }

        async Task<ExchangeCallResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, EnvironmentName, null))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, EnvironmentName, null, symbolName));
        }

        #endregion

        #region Open Interest client

        GetOpenInterestOptions IOpenInterestRestClient.GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, true);
        async Task<HttpResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            return HttpResult.Ok(result, new SharedOpenInterest(result.Data.FutureProductDetails!.PerpetualDetails?.OpenInterest ?? 0));
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

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false);
        async Task<HttpResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (request.ReduceOnly == true)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(PlaceFuturesOrderRequest.ReduceOnly), $"ReduceOnly flag is not available on {Exchange}, use ClosePositionAsync with quantity to reduce a position"));

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

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId.ToString()));
        }

        GetFuturesOrderOptions IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new GetFuturesOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, order.Data.Symbol),
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

        GetOpenFuturesOrdersOptions IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new GetOpenFuturesOrdersOptions(_exchangeName, true);
        async Task<HttpResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var expiryType = ((request.Symbol?.TradingMode ?? request.TradingMode) ?? TradingMode.PerpetualLinear) == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var orders = await Trading.GetOrdersAsync(
                symbols: request.Symbol == null ? Array.Empty<string>() : [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Open],
                symbolType: SymbolType.Futures,
                expiryType: expiryType,
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Select(x => new SharedFuturesOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), 
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

        GetFuturesClosedOrdersOptions IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, false, true, true, 1000);
        async Task<HttpResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetClosedFuturesOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesOrder[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var expiryType = request.Symbol!.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var result = await Trading.GetOrdersAsync(
                symbols: [request.Symbol!.GetSymbol(FormatSymbol)],
                orderStatus: [OrderStatus.Canceled, OrderStatus.Filled],
                symbolType: SymbolType.Futures,
                expiryType: expiryType,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesOrder[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                result.Data.Length,
                result.Data.Select(x => x.CreateTime),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedFuturesOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), 
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
                            }).ToArray(), nextPageRequest);
        }

        GetFuturesOrderTradesOptions IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new GetFuturesOrderTradesOptions(_exchangeName, true);
        async Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var orders = await Trading.GetUserTradesAsync(orderIds: [request.OrderId], ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data.Trades.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), 
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

        GetFuturesUserTradesOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new GetFuturesUserTradesOptions(_exchangeName, false, true, true, 100);
        async Task<HttpResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesUserTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 100;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await Trading.GetUserTradesAsync(
                symbols: [request.Symbol!.GetSymbol(FormatSymbol)],
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                cursor: pageParams.Cursor,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                () => result.Data.Cursor == null ? null : Pagination.NextPageFromCursor(result.Data.Cursor),
                result.Data.Trades.Length,
                result.Data.Trades.Select(x => x.Timestamp),
                request.StartTime,
                request.EndTime ?? DateTime.UtcNow,
                pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Trades, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), 
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
                            })
                       .ToArray(), nextPageRequest);
        }

        CancelFuturesOrderOptions IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new CancelFuturesOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data.OrderId!));
        }

        GetPositionsOptions IFuturesOrderRestClient.GetPositionsOptions { get; } = new GetPositionsOptions(_exchangeName, true);
        async Task<HttpResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetPositionsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedPosition[]>(Exchange, validationError);

            var tradingMode = request.Symbol?.TradingMode ?? request.TradingMode;
            if (tradingMode == null || request.TradingMode == TradingMode.PerpetualLinear)
            {
                var portfolioId = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "PortfolioId");
                if (portfolioId == default)
                    return HttpResult.Fail<SharedPosition[]>(Exchange, ArgumentError.Missing("PortfolioId", "PortfolioId is required as Exchange parameter for retrieving Perpetual futures balances"));

                var result = await Trading.GetPerpetualPositionsAsync(portfolioId, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, result.Data.Positions.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), x.Symbol, Math.Abs(x.NetQuantity), null)
                {
                    UnrealizedPnl = x.UnrealizedPnl.Value,
                    LiquidationPrice = x.LiquidationPrice.Value == 0 ? null : x.LiquidationPrice.Value,
                    Leverage = x.Leverage,
                    AverageOpenPrice = x.EntryVolumeWeightedAveragePrice.Value,
                    PositionMode = SharedPositionMode.HedgeMode,
                    PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
            else
            {
                var result = await Trading.GetFuturesPositionsAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedPosition[]>(result);

                return HttpResult.Ok(result, result.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, x.Symbol), x.Symbol, Math.Abs(x.NumberOfContracts), null)
                {
                    UnrealizedPnl = x.UnrealizedPnl,
                    AverageOpenPrice = x.AverageEntryPrice,
                    PositionMode = SharedPositionMode.HedgeMode,
                    PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                }).ToArray());
            }
        }

        ClosePositionOptions IFuturesOrderRestClient.ClosePositionOptions { get; } = new ClosePositionOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.ClosePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await Trading.ClosePositionAsync(
                symbol,
                quantity: request.Quantity,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        #endregion

        #region Klines Client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, false, true, true, 350, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.SixHours,
            SharedKlineInterval.OneDay);

        async Task<HttpResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;

            var validationError = SharedClient.GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 350;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest);

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                pageParams.StartTime ?? DateTime.UtcNow.Add(TimeSpan.FromSeconds(-((int)interval * 100))),
                pageParams.EndTime,
                pageParams.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime).AddSeconds(-(int)interval)),
                    result.Data.Length,
                    result.Data.Select(x => x.OpenTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                   .Select(x => 
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                   .ToArray(), nextPageRequest);
        }

        #endregion

        #region Fee Client
        GetFeeOptions IFeeRestClient.GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        async Task<HttpResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var symbolType = request.Symbol!.TradingMode == TradingMode.Spot ? SymbolType.Spot : SymbolType.Futures;
            var result = await Account.GetFeeInfoAsync(symbolType, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(result.Data.FeeTier.MakerFeeRate * 100, result.Data.FeeTier.TakerFeeRate * 100));
        }
        #endregion

        #region Spot Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, true);

        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
        }

        GetSpotTriggerOrderOptions ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new GetSpotTriggerOrderOptions(_exchangeName, true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        async Task<HttpResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicSpotId, EnvironmentName, null, order.Data.Symbol),
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

            if (orderStatus == OrderStatus.Open
                || orderStatus == OrderStatus.CancelQueued
                || orderStatus == OrderStatus.Pending)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        CancelSpotTriggerOrderOptions ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        #region Futures Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, true)
        {
        };

        async Task<HttpResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetTriggerOrderSide(request);
            var validationError = SharedClient.PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.SuccessResponse.OrderId));
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

        GetFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        async Task<HttpResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicFuturesId, EnvironmentName, null, order.Data.Symbol),
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

        CancelFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
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
            if (!accounts.Success)
                return null;

            var account = accounts.Data.Accounts.FirstOrDefault(x => x.Asset == asset);
            if (account == null)
                return null;

            return account.AccountId;
        }
    }
}
