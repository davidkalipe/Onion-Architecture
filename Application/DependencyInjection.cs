using Application.Features.ProductFeatures.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateProductCommand).Assembly);
    }
}