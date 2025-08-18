using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace EmployeesManagement.API.Midlewares;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

    public ExceptionsMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
    {
        _next = next;
        _environment = environment;
        _memoryCache = memoryCache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        
    }

    private bool IsRequestAllowed(HttpContext context)
    {
        
        return true;
    }
}