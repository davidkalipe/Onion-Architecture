using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Commands;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Barecode { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
    public string CustomerPhone { get; set; }

    public CreateProductCommand() { }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.Phonenumber == command.CustomerPhone)
                .FirstOrDefaultAsync();
            var product = new Product();
            product.BareCode = command.Barecode;
            product.Name = command.Name;
            product.Rate = command.Rate;
            product.Description = command.Description;
            product.CustomerId = customer.Id;
            _context.Products.Add(product);
            await _context.SaveChanges();
            return _context.Products.Count();
        }
    }
}