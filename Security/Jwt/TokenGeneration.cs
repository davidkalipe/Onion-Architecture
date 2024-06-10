using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Security.Jwt;

public class TokenGeneration : ITokenGenerator
{
    private readonly IJwtConfig _jwtConfig;

    // public JwtTokenGenerator(IJwtConfig jwtConfig)
    // {
    //     _jwtConfig = jwtConfig;
    // }

    public string CreateCustomerToken(Customer customer)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.MobilePhone, customer.Phonenumber)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.GetKey()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.GetIssuer(),
            audience: _jwtConfig.GetAudience(),
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}