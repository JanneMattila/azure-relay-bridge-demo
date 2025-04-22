var builder = WebApplication.CreateBuilder(args);

// Set Kestrel max request body size to 200 MB
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 200 * 1024 * 1024; // 200 MB
});

var app = builder.Build();
//app.UseHttpsRedirection();


app.MapGet("/", (HttpRequest httpRequest) =>
{
    // Check if the request has the Authorization header
    if (!httpRequest.Headers.TryGetValue("Authorization", out var authorizationHeader))
    {
        return Results.Unauthorized();
    }

    return Results.Ok("Response from Edge Service");
});

app.MapPost("/", async (HttpRequest httpRequest) =>
{
    // Check if the request has the Authorization header
    if (!httpRequest.Headers.TryGetValue("Authorization", out var authorizationHeader))
    {
        return Results.Unauthorized();
    }

    // Read the request body as binary
    using var ms = new MemoryStream();
    await httpRequest.Body.CopyToAsync(ms);
    var size = ms.Length;
    return Results.Ok($"Response from Edge Service to post of size: {size}");
});

app.Run();
