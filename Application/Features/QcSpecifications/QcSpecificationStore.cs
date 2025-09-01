using Microsoft.EntityFrameworkCore;
using VeriShip.Domain.Entities.QCSpecifications;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Application.Features.QcSpecifications;

public class QcSpecificationStore(IApplicationDbContextFactory dbContextFactory) : IQcSpecificationStore
{

    public async Task<IEnumerable<QcSpecification>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        var db = await dbContextFactory.CreateAsync(cancellationToken);
        var querySort = await db.QcSpecificationsSort.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        var order = querySort?.SelectMany(x=>x.Order).ToArray() ?? [];
        var query = await db
            .QcSpecifications
            .AsNoTracking()
            .Where(x=>x.Table==request.Table)
            .ToListAsync(cancellationToken: cancellationToken);
        var checks = query.OrderBy(item =>
            {
                var index = Array.IndexOf(order, item.Id);
                return index < 0 ? int.MaxValue : index;
            }
        );
        return checks;
    }


  
}

