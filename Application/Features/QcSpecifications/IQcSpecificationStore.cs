using VeriShip.Application.Features.QcSpecifications;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features;

public interface IQcSpecificationStore
{
    Task<IEnumerable<QcSpecification>> Handle(GetAllRequest request, CancellationToken cancellationToken);
}