using Ardalis.Result;
using VeriShip.Application.Features.Projects.Commands;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Domain.Entities.Projects;
using QcSpecificationResult = VeriShip.Domain.Entities.QCSpecifications.Result;
namespace VeriShip.Application.Features.Projects;

public interface IProjectStore
{
    Task<Result<Project>> Query(GetProject request, CancellationToken cancellationToken = default);
    Task<Result<IOrderedEnumerable<QcSpecificationResult>>> Query(GetDefaultQcSpecifications request, CancellationToken cancellationToken = default);
}