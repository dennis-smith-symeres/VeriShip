using VeriShip.Application.Features.QcSpecifications.Commands;
using VeriShip.Application.Features.QcSpecifications.Queries;
using VeriShip.Domain.Entities.QCSpecifications;
using Sort = VeriShip.Application.Features.QcSpecifications.Commands.Sort;

namespace VeriShip.Application.Features.QcSpecifications;

public interface IQcSpecificationStore
{
    Task<IEnumerable<QcSpecification>> Handle(GetAll request, CancellationToken cancellationToken);
    
    Task<int> Handle(Upsert command, CancellationToken cancellationToken);
    Task<int> Handle(Sort command, CancellationToken cancellationToken);
}