using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Railway.HttpClients.Registrars;
using Soenneker.Railway.GraphQlClientUtil.Abstract;

namespace Soenneker.Railway.GraphQlClientUtil.Registrars;

/// <summary>
/// A .NET thread-safe singleton GraphQL client
/// </summary>
public static class RailwayGraphQlClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="RailwayGraphQlClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddRailwayGraphQlClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddRailwayGraphQlHttpClientAsSingleton()
                .TryAddSingleton<IRailwayGraphQlClientUtil, RailwayGraphQlClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="RailwayGraphQlClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddRailwayGraphQlClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddRailwayGraphQlHttpClientAsSingleton()
                .TryAddScoped<IRailwayGraphQlClientUtil, RailwayGraphQlClientUtil>();

        return services;
    }
}

