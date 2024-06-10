namespace Application.Interfaces;

public interface ITokenValidator
{
    bool IsTokenValid(string token);
}