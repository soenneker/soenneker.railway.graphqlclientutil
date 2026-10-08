using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Railway.GraphQlClientUtil.Abstract;
using Soenneker.Railway.GraphQlClientUtil.Registrars;
using Soenneker.Railway.HttpClients.Abstract;
using System.Threading;

namespace Soenneker.Railway.GraphQlClientUtil.Tests;

public sealed class RailwayGraphQlClientUtilTests
{
    [Test]
    public async ValueTask ResolvesRailwayDependenciesAndReusesClient(CancellationToken cancellationToken)
    {
        IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Railway:ApiKey"] = "test-token" }).Build();
        await using var provider = new ServiceCollection().AddLogging().AddSingleton(config)
            .AddRailwayGraphQlClientUtilAsSingleton().BuildServiceProvider();
        var utility = provider.GetRequiredService<IRailwayGraphQlClientUtil>();
        var first = await utility.Get(cancellationToken: cancellationToken);
        if (!ReferenceEquals(first, await utility.Get(cancellationToken: cancellationToken)) || first.Project == null)
            throw new Exception("The Railway client is not cached or is missing typed operations.");
        var http = await provider.GetRequiredService<IRailwayGraphQlHttpClient>().Get(cancellationToken: cancellationToken);
        if (http.DefaultRequestHeaders.Authorization?.Parameter != "test-token")
            throw new Exception("The utility is not using Railway authentication.");
    }
}
