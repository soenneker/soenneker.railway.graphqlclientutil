using System;
using Soenneker.Railway.GraphQlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Railway.GraphQlClientUtil.Abstract;

/// <summary>
/// Provides a shared Railway GraphQL client backed by the configured Railway HTTP client.
/// </summary>
public interface IRailwayGraphQlClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>Gets the shared client. Authentication is configured through Railway:ApiKey on the HTTP client.</summary>
    ValueTask<RailwayGraphQlClient> Get(CancellationToken cancellationToken = default);
}

