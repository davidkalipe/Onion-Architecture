using System.IdentityModel.Tokens.Jwt;
using Application.Interfaces;

namespace Security.Jwt;

public class DecodeToken : IDecodeToken
{
    
    public string DecodeJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        if (jsonToken == null)
            return null;
        var phone = jsonToken.Claims
            .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone")?.Value;

        return phone;
    }
}