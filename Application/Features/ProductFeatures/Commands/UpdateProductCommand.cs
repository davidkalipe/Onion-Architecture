using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Commands;

public class UpdateProductCommand : IRequest<int>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;
        
        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(p => p.Id == command.Id).FirstOrDefault();
            if (product == null)
                return default;
            product.Name = command.Name;
            product.BareCode = command.Barcode;
            product.Rate = command.Rate;
            product.Description = command.Description;
            await _context.SaveChanges();
            return 1;
        }
    }
    
}