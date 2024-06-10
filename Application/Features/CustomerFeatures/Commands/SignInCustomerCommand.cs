using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CustomerFeatures.Commands;

public class SignInCustomerCommand : IRequest<Customer>
{
    public string Phonenumber { get; set; }
    public string Password { get; set; }

    public class SignInCustomerCommandHandler : IRequestHandler<SignInCustomerCommand, Customer>
    {
        private readonly IApplicationDbContext _context;

        public SignInCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Customer> Handle(SignInCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.Phonenumber == request.Phonenumber).FirstOrDefaultAsync();
            if (customer != null)
            {
                var checkPwd = BCrypt.Net.BCrypt.Verify(request.Password, customer.Password);
                if (checkPwd)
                    return customer;
            }
            return null;
        }
    }
    
}