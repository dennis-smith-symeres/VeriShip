using Ardalis.Result;
using VeriShip.Application.Features.Projects.Commands;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Domain.Entities.Projects;

namespace VeriShip.Application.Features.Projects;

public interface IProjectStore
{
  Task<Result<Project>> Query(GetProject request, CancellationToken cancellationToken);
}