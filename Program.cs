using CG_Assist_MCPServer.Middleware;
using CG_Assist_MCPServer.Tools;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<LeaveTools>();

var app = builder.Build();

app.UseMiddleware<ApiKeyValidationMiddleware>();

app.UseCors();

app.MapGet("/ping", () => "Leave MCP Server is running...");

app.MapMcp("/mcp");

await app.RunAsync();