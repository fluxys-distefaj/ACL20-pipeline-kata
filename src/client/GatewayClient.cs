using System.Net.Http.Json;
using Contracts;

namespace Client;     // match your client namespace

public class GatewayClient
{
    private readonly HttpClient _http;
    public GatewayClient(HttpClient http) => _http = http;

    public Task<PingResponse?> GetPingAsync()
        => _http.GetFromJsonAsync<PingResponse>("/api/ping");
}