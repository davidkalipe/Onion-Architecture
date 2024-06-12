using Application.Interfaces;
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
        service.AddScoped<DecodeToken>();
        service.AddScoped<IJwtConfig>(p => p.GetService<JwtConfig>()!);
        service.AddScoped<ITokenGenerator>(p => p.GetService<TokenGeneration>()!);
        service.AddScoped<IDecodeToken>(p => p.GetService<DecodeToken>()!);
        service.AddScoped<ITokenValidator>(p => p.GetService<TokenValidator>()!);
    }
}