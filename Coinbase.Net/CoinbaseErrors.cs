using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Net
{
    internal static class CoinbaseErrors
    {
        public static ErrorCollection Errors { get; } = new ErrorCollection(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "PERMISSION_DENIED"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "INVALID_ARGUMENT"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "INSUFFICIENT_FUND"),

                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Order configuration rejected", "UNSUPPORTED_ORDER_CONFIGURATION"),
            ]
        );
    }
}
