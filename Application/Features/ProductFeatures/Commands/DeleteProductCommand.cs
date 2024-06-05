using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Commands;

public class DeleteProductCommand : IRequest<int>
{
    public Guid Id { get; set; }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == command.Id).FirstOrDefaultAsync();
            if (product == null) return default;
            _context.Products.Remove(product);
            await _context.SaveChanges();
            return 1;
        }
    }
    
}