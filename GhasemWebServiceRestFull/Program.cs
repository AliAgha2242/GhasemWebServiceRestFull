using Application.ServiceContract;
using GhasemWebServiceRestFull.Dependency;
using Infrastructure.SqlConnectionManagerGroup;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

var app = builder.Build();

// Compression must wrap the endpoint response so it can negotiate
// Accept-Encoding and compress the JSON stream exactly once.
app.UseResponseCompression();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription();

app.MapGet("/getTest", async (
    [FromKeyedServices("Mrs")] ITaminReports reports,
    string fromdate,
    string toDate,
    HttpContext context) =>
{
    await WriteSalesResponseAsync(
        context,
        reports,
        fromdate,
        toDate);

    return Results.Empty;
});

app.MapGet("/getTestStr", async (
    [FromKeyedServices("Mrs")] ITaminReports reports,
    HttpContext context) =>
{
    // Kept for compatibility with the existing test endpoint.
    await WriteSalesResponseAsync(
        context,
        reports,
        "14050101",
        "14050631");

    return Results.Empty;
});

app.Run();

static async Task WriteSalesResponseAsync(
    HttpContext context,
    ITaminReports reports,
    string fromDate,
    string toDate)
{
    context.Response.StatusCode = StatusCodes.Status200OK;
    context.Response.ContentType = "application/json; charset=utf-8";
    context.Response.Headers.CacheControl = "no-store";

    var rows = reports.GetSalesAsync(
        453.935m,
        fromDate,
        toDate,
        context.RequestAborted);

    // System.Text.Json enumerates IAsyncEnumerable incrementally.
    // The HTTP body is streamed, while the client still receives one valid JSON array.
    await JsonSerializer.SerializeAsync(
        context.Response.Body,
        rows,
        cancellationToken: context.RequestAborted);
}
