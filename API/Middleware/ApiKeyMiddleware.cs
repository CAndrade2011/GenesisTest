namespace API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "API-Key";
    private const string ValidApiKey = "Genesis-API-Key-2025"; 

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (RequiresApiKey(context.Request.Path))
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key não fornecida");
                return;
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
            if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != ValidApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key inválida");
                return;
            }
        }

        await _next(context);
    }

    private static bool RequiresApiKey(PathString path)
    {
        return path.StartsWithSegments("/calculadora");
    }
} 