using Microsoft.Extensions.Configuration;
using Application.Interfaces;

namespace Security.Jwt;

public class JwtConfig : IJwtConfig
{
    private readonly IConfiguration _configuration;

    public JwtConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetKey() => _configuration["AppSettings:Key"]!;
    public string GetIssuer() => _configuration["AppSettings:Issuer"]!;
    public string GetAudience() => _configuration["AppSettings:Audience"]!;
    
}