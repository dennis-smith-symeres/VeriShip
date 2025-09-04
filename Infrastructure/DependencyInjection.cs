using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        //services.AddScoped<ISignals, Signals>();
        return services;
    }
    
    
}