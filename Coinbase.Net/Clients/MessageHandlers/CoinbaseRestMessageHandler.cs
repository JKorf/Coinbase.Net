using Coinbase.Net.Objects;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageConverters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coinbase.Net.Clients.MessageHandlers
{
    internal class CoinbaseRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CoinbaseExchange._serializerContext);

        public CoinbaseRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override async ValueTask<Error> ParseErrorResponse(
            int httpStatusCode,
            object? state,
            HttpResponseHeaders responseHeaders,
            Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream, state).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            string? error = document!.RootElement.TryGetProperty("error", out var errorProp) ? errorProp.GetString() : null;
            if (error != null)
            {
                string? errorMsg = document.RootElement.TryGetProperty("message", out var errorMsgProp) ? errorMsgProp.GetString() : null;
                return new ServerError(error, _errorMapping.GetErrorInfo(error, errorMsg));
            }

            if (!document!.RootElement.TryGetProperty("errors", out var errorsProp))
                return new ServerError(ErrorInfo.Unknown);

            var error0Prop = errorsProp[0];

            if (error0Prop.TryGetProperty("id", out var idProp))
            {
                var id = idProp.GetString();
                var msg = error0Prop.GetProperty("message").GetString();
                return new ServerError(id!, _errorMapping.GetErrorInfo(id!, msg));
            }

            return new ServerError(ErrorInfo.Unknown);
        }

        public override async ValueTask<ServerRateLimitError> ParseErrorRateLimitResponse(
            int httpStatusCode,
            object? state,
            HttpResponseHeaders responseHeaders, 
            Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream, state).ConfigureAwait(false);
            if (parseError != null)
                return _emptyRateLimitError;

            var reset = responseHeaders.SingleOrDefault(x => x.Key.Equals("x-ratelimit-reset", StringComparison.InvariantCultureIgnoreCase));
            if (reset.Key == null)
                return await base.ParseErrorRateLimitResponse(httpStatusCode, state, responseHeaders, responseStream).ConfigureAwait(false);

            if (!int.TryParse(reset.Value.Single(), out var seconds))
                return await base.ParseErrorRateLimitResponse(httpStatusCode, state, responseHeaders, responseStream).ConfigureAwait(false);

            var error = new ServerRateLimitError();
            error.RetryAfter = DateTime.UtcNow.AddSeconds(seconds);
            return error;
        }
    }
}
