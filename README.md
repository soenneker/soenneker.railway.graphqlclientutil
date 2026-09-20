# Soenneker.Railway.GraphQlClientUtil

A cached, typed Railway GraphQL client for .NET 10 dependency injection.

```csharp
using Soenneker.Railway.GraphQlClient;
using Soenneker.Railway.GraphQlClientUtil.Abstract;
using Soenneker.Railway.GraphQlClientUtil.Registrars;

services.AddRailwayGraphQlClientUtilAsSingleton();
// IConfiguration must contain Railway:ApiKey (environment variable Railway__ApiKey).
var utility = serviceProvider.GetRequiredService<IRailwayGraphQlClientUtil>();
var client = await utility.Get(cancellationToken);
var response = await client.GetProject.Execute(new GetProjectVariables { Id = projectId }, cancellationToken);
```

The registrar also registers `IRailwayGraphQlHttpClient`. It uses the HTTP client's Railway authentication and endpoint configuration, without adding a second authentication layer. Singleton and scoped utility registrations are available; HTTP clients are cached by the singleton HTTP service. Disposing the utility releases its cached client wrapper; the HTTP service owns its transport lifetime.

For local development, keep the `soenneker.railway.graphqlclient` and `soenneker.railway.httpclients` checkouts beside this repository. The project uses sibling project references when available and version 4.0.0 package references otherwise. Publish those two dependencies before building this repository standalone against NuGet.
