using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Projects;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Application.Features.QcRequests.Queries;
using VeriShip.Application.Features.Signals;
using VeriShip.Domain.Entities.QcRequests;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Application.Features.QcRequests;

public class QcRequestStore(IApplicationDbContextFactory dbContextFactory, IProjectStore projectStore, ILogger<QcRequestStore> logger) : IQcRequestStore
{
    public async Task<Result<IEnumerable<QcRequest>>> Query(GetRequests request)
    {
        var projectResult = await projectStore.Query(request, CancellationToken.None);
        if (!projectResult.IsSuccess)
        {
            return projectResult.Map();
        }

        try
        {
            var db = await dbContextFactory.CreateAsync(CancellationToken.None);
            var queryable = db.QcRequests.AsNoTracking().Where(x => x.ProjectId == projectResult.Value.Id);
            var requests = await queryable.ToListAsync(CancellationToken.None);
            return Result<IEnumerable<QcRequest>>.Success(requests);
        }
        catch (Exception e)
        {
          logger.LogError(e, "Error getting all requests");
          return Result.Error(e.Message);
        }
        
    }
}