using System.Net;
using Client;
using Contracts;
using PactNet;
using PactNet.Output.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Client.Contract.Tests;

public class GatewayContractTests
{
    private readonly IPactBuilderV4 _pact;

    public GatewayContractTests(ITestOutputHelper output)
    {
        var config = new PactConfig
        {
            PactDir = Path.Combine("..", "..", "..", "..", "..", "pacts"),  // repo-root/pacts
            Outputters = new[] { new XunitOutput(output) },
        };
        _pact = Pact.V4("client", "gateway", config).WithHttpInteractions();
    }

    [Fact]
    public async Task GetPingAsync_returns_pong()
    {
        _pact
            .UponReceiving("a ping request")
            .WithRequest(HttpMethod.Get, "/api/ping")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new { message = "pong" });

        await _pact.VerifyAsync(async ctx =>
        {
            var client = new GatewayClient(new HttpClient { BaseAddress = ctx.MockServerUri });
            var ping = await client.GetPingAsync();   // YOUR real client method
            Assert.Equal("pong", ping!.Message);
        });
    }
}