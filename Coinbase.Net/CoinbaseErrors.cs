using CryptoExchange.Net.Objects.Errors;

namespace Coinbase.Net
{
    internal static class CoinbaseErrors
    {
        public static ErrorMapping Errors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "PERMISSION_DENIED"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "INVALID_ARGUMENT"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Subject not found", "NOT_FOUND"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "INSUFFICIENT_FUND"),

                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Order configuration rejected", "UNSUPPORTED_ORDER_CONFIGURATION"),
            ]
        );
    }
}
