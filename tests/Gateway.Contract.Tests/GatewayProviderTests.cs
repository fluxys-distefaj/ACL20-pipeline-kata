using Microsoft.AspNetCore.Builder;
using PactNet.Output.Xunit;
using PactNet.Verifier;
using Xunit;
using Xunit.Abstractions;

namespace Gateway.Contract.Tests;

public class GatewayProviderTests : IAsyncLifetime
{
    private const string ProviderUri = "http://127.0.0.1:9001";   // a real TCP port
    private WebApplication _app = null!;
    private readonly ITestOutputHelper _output;

    public GatewayProviderTests(ITestOutputHelper output) => _output = output;

    public async Task InitializeAsync()
    {
        _app = GatewayApp.Build(Array.Empty<string>());   // the REAL gateway pipeline
        _app.Urls.Clear();
        _app.Urls.Add(ProviderUri);
        await _app.StartAsync();                          // NOT WebApplicationFactory
    }

    public async Task DisposeAsync() => await _app.StopAsync();

    [Fact]
    public void Gateway_honours_the_client_contract()
    {
        var pact = Path.Combine("..", "..", "..", "..", "..", "pacts", "client-gateway.json");

        using var verifier = new PactVerifier("gateway",
            new PactVerifierConfig { Outputters = new[] { new XunitOutput(_output) } });

        verifier
            .WithHttpEndpoint(new Uri(ProviderUri))
            .WithFileSource(new FileInfo(pact))   // file-based, no broker
            .Verify();
    }
}