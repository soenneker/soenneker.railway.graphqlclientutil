using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Railway.GraphQlClient;
using Soenneker.Railway.GraphQlClientUtil.Abstract;
using Soenneker.Railway.HttpClients.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Railway.GraphQlClientUtil;

public sealed class RailwayGraphQlClientUtil : IRailwayGraphQlClientUtil
{
    private readonly AsyncSingleton<RailwayGraphQlClient> _client;

    public RailwayGraphQlClientUtil(IRailwayGraphQlHttpClient httpClient)
    {
        _client = new AsyncSingleton<RailwayGraphQlClient>(async token =>
            new RailwayGraphQlClient(new GraphQlHttpClient(await httpClient.Get(token))));
    }

    public ValueTask<RailwayGraphQlClient> Get(CancellationToken cancellationToken = default) => _client.Get(cancellationToken);
    public void Dispose() => _client.Dispose();
    public ValueTask DisposeAsync() => _client.DisposeAsync();
}

