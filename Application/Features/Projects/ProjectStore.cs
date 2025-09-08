
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using RefitClient.Responses.Attributes;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Projects.Commands;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Application.Features.QcSpecifications.Commands;
using VeriShip.Application.Features.Signals;
using VeriShip.Domain.Entities.Projects;
using VeriShip.Infrastructure.Persistence;


namespace VeriShip.Application.Features.Projects;

public class ProjectStore(
    IApplicationDbContextFactory dbContextFactory, 
    ILogger<ProjectStore> logger,
    ISignalsStore signalsStore
    ) : IProjectStore
{
    public async Task<Result<Project>> Query(GetProject request, CancellationToken cancellationToken)
    {
        var notebookResult = await signalsStore.Query(request);
        if (!notebookResult.IsSuccess)
        {
            return notebookResult.Map();
        }
        var userResult = request.ClaimsPrincipal.ToUserResult();
        if (!userResult.IsSuccess)
        {
            return userResult.Map();
        }

        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var project = db.Projects.FirstOrDefault(x => x.ProjectNumber == request.ProjectNumber);
            if (project is null)
            {
                project = new Project()
                {
                    Active = true,
                    ProjectNumber = request.ProjectNumber,
                    SignalsId = notebookResult.Value.Id,
                    Client = notebookResult.Value.Client,
                };
                db.Projects.Add(project);
                await db.SaveChangesAsync(userResult.Value.Name, cancellationToken);
            }
            return Result<Project>.Success(project);
        }
        catch (Exception e)
        {
          logger.LogError(e, "Error getting project");
            return Result<Project>.Error(e.Message);
        }
    }
}