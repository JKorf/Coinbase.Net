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
    internal partial class CoinbaseRestClientAdvancedTradeSharedApi
    {
        #region Asset client
        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, false);

        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var fiatAssets = _api.ExchangeData.GetFiatAssetsAsync(ct: ct);
            var cryptoAssets = _api.ExchangeData.GetCryptoAssetsAsync(ct: ct);
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

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var cryptoAssets = await _api.ExchangeData.GetCryptoAssetsAsync(ct: ct).ConfigureAwait(false);
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

            var fiatAssets = await _api.ExchangeData.GetFiatAssetsAsync(ct: ct).ConfigureAwait(false);
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
    }
}
