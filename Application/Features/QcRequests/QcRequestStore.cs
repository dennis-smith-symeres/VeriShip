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
    public async Task<Result<IEnumerable<QcRequest>>> Query(GetRequests request, CancellationToken cancellationToken = default)
    {
        var projectResult = await projectStore.Query(request, cancellationToken);
        if (!projectResult.IsSuccess)
        {
            return projectResult.Map();
        }

        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var queryable = db.QcRequests.AsNoTracking().Where(x => x.ProjectId == projectResult.Value.Id);
            var requests = await queryable.ToListAsync(cancellationToken);
            return Result<IEnumerable<QcRequest>>.Success(requests);
        }
        catch (Exception e)
        {
          logger.LogError(e, "Error getting all requests");
          return Result.Error(e.Message);
        }
        
    }
    
    public async Task<Result<QcRequest>> Query(GetRequest request, CancellationToken cancellationToken = default)
    {
        var projectResult = await projectStore.Query(request,cancellationToken);
        if (!projectResult.IsSuccess)
        {
            return projectResult.Map();
        }

        if (request.RequestId < 1)
        {
            var qcRequest = new QcRequest()
            {
                ProjectId = projectResult.Value.Id,
                Id = -1,
                Active = true,
                Label = DateTime.Now.ToString("yyyyMMddHHmmss"),
                AttachmentIds = [],
                Items = []
            };
            return Result<QcRequest>.Success(qcRequest);
        }
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var queryable = db.QcRequests.AsNoTracking().Where(x => x.ProjectId == projectResult.Value.Id
            && x.Id == request.RequestId
            );
            var requests = await queryable.FirstOrDefaultAsync(cancellationToken);
            if (requests is null)
            {
                return Result<QcRequest>.NotFound("Request not found");
            }
            return Result<QcRequest>.Success(requests);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting all request");
            return Result.Error(e.Message);
        }
        
    }
}

