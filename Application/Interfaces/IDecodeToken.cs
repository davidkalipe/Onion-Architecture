namespace Application.Interfaces;

public interface IDecodeToken
{
    string DecodeJwt(string token);
}