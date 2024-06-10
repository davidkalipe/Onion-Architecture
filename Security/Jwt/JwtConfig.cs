using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Security.Jwt;

public class JwtConfig
{
    private readonly IConfiguration _configuration;

    public JwtConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateUserToken(Customer customer)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.MobilePhone, customer.Phonenumber)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Key"]!));
        var issuer = _configuration["AppSettings:Issuer"];
        var audience = _configuration["AppSettings:Audience"];
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
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
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (Exception e)
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
            ValidIssuer = _configuration["AppSettings:Issuer"],
            ValidAudience = _configuration["AppSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Key"]!))
        };
    }
}