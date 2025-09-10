using Ardalis.Result;
using VeriShip.Application.Features.QcSpecifications.Commands;
using VeriShip.Application.Features.QcSpecifications.Queries;
using VeriShip.Domain.Entities.QCSpecifications;
using Sort = VeriShip.Application.Features.QcSpecifications.Commands.Sort;

namespace VeriShip.Application.Features.QcSpecifications;

public interface IQcSpecificationStore
{
    Task<Result<IEnumerable<QcSpecification>>> Query(GetAll request, CancellationToken cancellationToken = default);
    Task<Result<int>> Handle(Upsert command, CancellationToken cancellationToken = default);
    Task<Result<int>> Handle(Sort command, CancellationToken cancellationToken = default);
}