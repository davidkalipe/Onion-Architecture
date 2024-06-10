using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.CustomerFeatures.Commands;

public class SignUpCustomerCommand : IRequest<Customer>
{
    public string Fullname { get; set; }
    public string Phonenumber { get; set; }
    public string Password { get; set; }


    public class SignUpCustomerCommandHandler : IRequestHandler<SignUpCustomerCommand, Customer>
    {
        private readonly IApplicationDbContext _context;
        public SignUpCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Customer> Handle(SignUpCustomerCommand request, CancellationToken cancellationToken)
        {
            var hashPwd = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var customer = new Customer();
            customer.Fullname = request.Fullname;
            customer.Phonenumber = request.Phonenumber;
            customer.Password = hashPwd;
            _context.Customers.Add(customer);
            await _context.SaveChanges();
            return customer;
        }
    }
}