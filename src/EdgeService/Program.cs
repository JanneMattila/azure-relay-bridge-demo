var builder = WebApplication.CreateBuilder(args);

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

app.Run();
