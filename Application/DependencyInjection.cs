using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NeoSmart.Caching.Sqlite;
using RefitClient;
using VeriShip.Application.Features;
using VeriShip.Application.Features.Projects;
using VeriShip.Application.Features.QcSpecifications;
using VeriShip.Application.Features.Signals;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace VeriShip.Application;

public static class DependencyInjection
{
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IQcSpecificationStore, QcSpecificationStore>();
        services.AddScoped<IProjectStore, ProjectStore>();
        services.AddScoped<ISignalsStore, SignalsStore>();
        services.AddSignalsClient();
        services.AddMemoryCache();
        
        services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
        {
            Duration = TimeSpan.FromMinutes(10),
            Priority = CacheItemPriority.High,
            FactoryHardTimeout = TimeSpan.FromSeconds(2),
            FactorySoftTimeout = TimeSpan.FromMilliseconds(300),
            IsFailSafeEnabled = true,
            FailSafeMaxDuration =  TimeSpan.FromHours(2),
        })
            .WithSerializer(
                new FusionCacheSystemTextJsonSerializer()
                )
            .WithDistributedCache(
                new SqliteCache(new SqliteCacheOptions { CachePath = @"C:\temp\veriship\cache.db" })
            );
        return services;
    }

  
}