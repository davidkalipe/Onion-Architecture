using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenGenerator
{
    string CreateCustomerToken(Customer customer);
}