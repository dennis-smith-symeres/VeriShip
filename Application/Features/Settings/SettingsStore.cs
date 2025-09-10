using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeriShip.Application.Features.Settings.Queries;
using VeriShip.Domain.Entities.Settings;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Application.Features.Settings;

public class SettingsStore(IApplicationDbContextFactory dbContextFactory,  ILogger<SettingsStore> logger) : ISettingsStore
{
    public async Task<Result<Setting>> Query(GetSettings request, CancellationToken cancellationToken = default)
    {
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var settings = db.Settings.AsNoTracking().FirstOrDefault();
            if (settings == null)
            {
                return Result<Setting>.NotFound("Settings not found");
            }

            return new(settings);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting settings");
            return Result.Error(e.Message);
        }
        
    }
}

