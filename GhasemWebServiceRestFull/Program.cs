using Application.ServiceContract;
using Application.ServiceImplement;
using GhasemWebServiceRestFull.Dependency;
using Infrastructure.SqlConnectionManagerGroup;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
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
    options.Level = System.IO.Compression.CompressionLevel.Optimal;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.MapOpenApi();
//}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
app.MapGet("/",()=> Results.Redirect("/swagger")).ExcludeFromDescription();
app.MapGet("/getTest", async (
    [FromKeyedServices("Mrs")] SqlConnectionManager sqlConnection,string fromdate,string toDate) =>
{
    ITaminReports reports = new TaminReports(sqlConnection);

    var res = await reports.GetSalesAsync(
        453.935m, fromdate, toDate);

    byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(res);

    using var outputStream = new MemoryStream();

    using (var gzip = new GZipStream(
        outputStream,
        CompressionLevel.SmallestSize,
        leaveOpen: true))
    {
        await gzip.WriteAsync(bytes);
    }

    return Results.Bytes(
        outputStream.ToArray(),
        contentType: "application/json");
});


app.MapGet("/getTestStr", async (
    [FromKeyedServices("Mrs")] SqlConnectionManager sqlConnection,
    HttpContext context) =>
{
    ITaminReports reports = new TaminReports(sqlConnection);

    var res = await reports.GetSalesAsync(
        453.935m, "14050101", "14050631");

    context.Response.StatusCode = 200;
    context.Response.ContentType = "application/json";
    context.Response.Headers.ContentEncoding = "br";

    await using var brotli = new BrotliStream(
        context.Response.Body,
        CompressionLevel.Fastest,
        leaveOpen: true);

    await JsonSerializer.SerializeAsync(
        brotli,
        res,
        cancellationToken: context.RequestAborted);

    await brotli.FlushAsync();

    return Results.Empty;
});



app.UseResponseCompression();
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
