using Microsoft.Extensions.DependencyInjection;
using Security.Jwt;

namespace Security;

public static class DependencyInjection
{
    public static void AddSecurity(this IServiceCollection service)
    {
        service.AddScoped<JwtConfig>();
        service.AddScoped<TokenGeneration>();
        service.AddScoped<TokenValidator>();
    }
}