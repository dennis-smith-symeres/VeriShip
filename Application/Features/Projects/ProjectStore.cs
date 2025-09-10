using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RefitClient.Responses.Attributes;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Projects.Commands;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Application.Features.QcSpecifications.Commands;
using VeriShip.Application.Features.Signals;
using VeriShip.Domain.Entities.Projects;
using VeriShip.Domain.Entities.QCSpecifications;
using VeriShip.Infrastructure.Persistence;
using Result = Ardalis.Result.Result;
using QcSpecificationResult = VeriShip.Domain.Entities.QCSpecifications.Result;

namespace VeriShip.Application.Features.Projects;

public class ProjectStore(
    IApplicationDbContextFactory dbContextFactory,
    ILogger<ProjectStore> logger,
    ISignalsStore signalsStore
) : IProjectStore
{
    public async Task<Result<Project>> Query(GetProject request, CancellationToken cancellationToken = default)
    {
        var userResult = request.ClaimsPrincipal.ToUserResult();
        if (!userResult.IsSuccess)
        {
            return userResult.Map();
        }

        var notebookResult = await signalsStore.Query(request);
        if (!notebookResult.IsSuccess)
        {
            return notebookResult.Map();
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

    public async Task<Result<IOrderedEnumerable<QcSpecificationResult>>> Query(GetDefaultQcSpecifications request,
        CancellationToken cancellationToken = default)
    {
        //no need to check project permissions here
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var querySort = await db.QcSpecificationsSort.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
            var order = querySort?.SelectMany(x => x.Order).ToArray() ?? [];
            var results = await db.ProjectQcRequestItemResults.AsNoTracking()
                .Where(x => x.Active)
                .Include(x => x.QcSpecification)
                .Where(x => x.Project.ProjectNumber == request.ProjectNumber)
                .Select(x => new QcSpecificationResult()
                {
                    QcSpecificationId = x.QcSpecificationId,
                    Value = x.Value,
                    Acceptance = x.Acceptance,
                    Active = x.Active,
                    QcSpecification = x.QcSpecification,
                })
                .ToListAsync(cancellationToken: cancellationToken);
            if (results.Count > 0)
            {
                var orderedResults = results.OrderBy(item =>
                    {
                        var index = Array.IndexOf(order, item.QcSpecificationId);
                        return index < 0 ? int.MaxValue : index;
                    }
                );

                return new(orderedResults);
            }

            var defaultResults = await db.QcSpecifications
                .AsNoTracking()
                .Where(c => c.IsDefault)
                .Select(x => new QcSpecificationResult()
                {
                    QcSpecificationId = x.Id,
                    Value = "",
                    Acceptance = x.Acceptance,
                    Active = true,
                    QcSpecification = x,
                }).ToListAsync(cancellationToken: cancellationToken);

            var orderedDefaultResults = defaultResults.OrderBy(item =>
                {
                    var index = Array.IndexOf(order, item.QcSpecificationId);
                    return index < 0 ? int.MaxValue : index;
                }
            );


            return new(orderedDefaultResults);
        }
        catch (Exception e)
        {
            return Result<IOrderedEnumerable<QcSpecificationResult>>.Error(e.Message);
        }
    }

    public async Task<Result<int>> Handle(SaveDefaultQcSpecifications command,
        CancellationToken cancellationToken = default)
    {
        var userResult = command.ClaimsPrincipal.ToUserResult();
        if (!userResult.IsSuccess)
        {
            return userResult.Map();
        }

        var notebookResult = await signalsStore.Query(command);
        if (!notebookResult.IsSuccess)
        {
            return notebookResult.Map();
        }

        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var project = await db.Projects
                .Include(x => x.Results)
                .FirstOrDefaultAsync(x => x.ProjectNumber == command.ProjectNumber, cancellationToken);
       
            // Step 1: Update or add child records
            foreach (var updatedChild in command.QCResults)
            {
                var existingChild = project.Results.FirstOrDefault(c => c.QcSpecificationId == updatedChild.QcSpecificationId);
                if (existingChild != null)
                {
                    // Update existing child
                    existingChild.Acceptance = updatedChild.Acceptance;
                    existingChild.Value = updatedChild.Value;
                    existingChild.Active = true;
                    // Update other fields as necessary...
                }
                else
                {
                    // Add new child
                    project.Results.Add(new ProjectResult()
                    {
                        QcSpecificationId = updatedChild.QcSpecificationId,
                        Active = true,
                        Acceptance = updatedChild.Acceptance,
                        Value = updatedChild.Value
                        // Set other fields as necessary...
                    });
                }
            }
            // Step 2: Remove child records no longer in the updated list
            var updatedChildIds = command.QCResults.Select(c => c.QcSpecificationId).ToList();
            var childrenToRemove = project.Results
                .Where(existingChild => !updatedChildIds.Contains(existingChild.QcSpecificationId))
                .ToList();

            foreach (var childToRemove in childrenToRemove)
            {
                project.Results.Remove(childToRemove);
            }
            await db.SaveChangesAsync(userResult.Value.Name, cancellationToken);
            return new(project.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting project");
            return Result<int>.Error(e.Message);
        }
       
    }
}