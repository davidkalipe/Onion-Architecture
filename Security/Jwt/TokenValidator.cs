using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Security.Jwt;

public class TokenValidator : ITokenValidator
{
    private readonly IJwtConfig _jwtConfig;

    public TokenValidator(IJwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }

    public bool IsTokenValid(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentException("Token is null or empty.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtConfig.GetIssuer(),
            ValidAudience = _jwtConfig.GetAudience(),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.GetKey()))
        };
    }
}