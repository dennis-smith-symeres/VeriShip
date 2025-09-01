using Microsoft.Extensions.DependencyInjection;
using VeriShip.Application.Features;
using VeriShip.Application.Features.QcSpecifications;

namespace VeriShip.Application;

public static class DependencyInjection
{
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IQcSpecificationStore, QcSpecificationStore>();
        return services;
    }

  
}