namespace Application.Interfaces;

public interface IJwtConfig
{
    string GetKey();
    string GetIssuer();
    string GetAudience();
}