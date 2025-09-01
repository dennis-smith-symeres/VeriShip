using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VeriShip.Infrastructure.Configuration;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>((p, m) =>
        {
            m.UseSqlServer(configuration.GetConnectionString("VeriShip"),
                b => b.MigrationsAssembly("VeriShip.WebApp")
            );
        });
        services.AddScoped<IApplicationDbContextFactory, ApplicationDbContextFactory>();

        return services;
    }
    
    
}