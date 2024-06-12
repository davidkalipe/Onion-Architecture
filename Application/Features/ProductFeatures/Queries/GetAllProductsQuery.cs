using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
    public string CustomerPhonenumber { get; set; }
    
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.Phonenumber == query.CustomerPhonenumber).FirstOrDefaultAsync();
            if(customer != null)
            {
                var productlist = await _context.Products.Where(p=>p.CustomerId == customer.Id).ToListAsync();
                if (productlist == null) return null;
                return productlist.AsReadOnly();
            }
            return null;
        }
    }
}