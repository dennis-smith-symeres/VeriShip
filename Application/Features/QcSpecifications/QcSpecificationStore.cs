using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using VeriShip.Application.Features.QcSpecifications.Commands;
using VeriShip.Application.Features.QcSpecifications.Queries;
using VeriShip.Domain.Entities.QCSpecifications;
using VeriShip.Infrastructure.Persistence;
using Sort = VeriShip.Application.Features.QcSpecifications.Commands.Sort;

namespace VeriShip.Application.Features.QcSpecifications;

public class QcSpecificationStore(IApplicationDbContextFactory dbContextFactory) : IQcSpecificationStore
{
    public async Task<Result<IEnumerable<QcSpecification>>> Query(GetAll request, CancellationToken cancellationToken)
    {
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var querySort = await db.QcSpecificationsSort.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
            var order = querySort.SelectMany(x => x.Order).ToArray();
            var query = await db
                .QcSpecifications
                .AsNoTracking()
                .Where(x => x.Table == request.Table)
                .ToListAsync(cancellationToken: cancellationToken);
            var specifications = query
                .OrderBy(item =>
                    {
                        var index = Array.IndexOf(order, item.Id);
                        return index < 0 ? int.MaxValue : index;
                    }
                );
            return new(specifications);
        }
        catch (Exception e)
        {
           return Result<IEnumerable<QcSpecification>>.Error(e.Message);
        }
    }

    public async Task<Result<int>> Handle(Upsert command, CancellationToken cancellationToken)
    {
        
        var db = await dbContextFactory.CreateAsync(cancellationToken);

        var specification = command.Specification.Id == 0
            ? new QcSpecification()
            {
                Category = command.Specification.Category.Trim(),
                Acceptance = command.Specification.Acceptance.Trim()
            }
            : await db.QcSpecifications
                .FirstOrDefaultAsync(c => c.Id == command.Specification.Id, cancellationToken: cancellationToken);

        if (specification == null)
        {
            return Result<int>.NotFound("Specification not found");

        }

        specification.Table = command.Specification.Table;
        specification.Comment = command.Specification.Comment?.Trim();
        specification.Category = command.Specification.Category.Trim();
        specification.Technique = command.Specification.Technique?.Trim();
        specification.Acceptance = command.Specification.Acceptance.Trim();
        specification.Active = true;
        specification.Values = command.Specification.Values.Where(x => string.IsNullOrEmpty(x) is false)
            .Select(x => x.Trim()).ToList();
        specification.IsDefault = command.Specification.IsDefault;
        specification.AllowCustomValue = command.Specification.AllowCustomValue;
        specification.AllowCustomAcceptance = command.Specification.AllowCustomAcceptance;
        if (command.Specification.Id == 0)
        {
            db.QcSpecifications.Add(specification);
        }

        try
        {
            await db.SaveChangesAsync(command.User, cancellationToken);
        }
        catch (Exception e)
        {
            return Result<int>.Error($"Error saving changes: {e.Message}");

        }

        return new(specification.Id);

    }

    public async Task<Result<int>> Handle(Sort command, CancellationToken cancellationToken)
    {
        var db = await dbContextFactory.CreateAsync(cancellationToken);
        var sort = await db.QcSpecificationsSort.FirstOrDefaultAsync(x => x.Table == command.Table,
            cancellationToken: cancellationToken);

        if (sort == null)
        {
            sort = new Domain.Entities.QCSpecifications.Sort()
            {
                Table = command.Table,
                Order = []
            };
            db.QcSpecificationsSort.Add(sort);
        }

        sort.Order = command.Order;

        try
        {
            await db.SaveChangesAsync(command.User, cancellationToken);
        }
        catch (Exception e)
        {
            return Result<int>.Error($"Error saving order: {e.Message}");

        }

        return new( sort.Id);
    }
}