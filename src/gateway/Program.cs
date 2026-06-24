using Contracts;

var app = GatewayApp.Build(args);
app.Run();

public static class GatewayApp
{
    public static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // ... your existing services + YARP reverse-proxy config ...
        var app = builder.Build();
        // ... your existing reverse-proxy mapping ...

        app.MapGet("/api/ping", () => new PingResponse("bou"));   // the contract endpoint
        return app;
    }
}