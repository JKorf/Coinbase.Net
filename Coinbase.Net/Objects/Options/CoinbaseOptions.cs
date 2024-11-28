using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Coinbase options
    /// </summary>
    public class CoinbaseOptions : LibraryOptions<CoinbaseRestOptions, CoinbaseSocketOptions, ApiCredentials, CoinbaseEnvironment>
    {
    }
}
