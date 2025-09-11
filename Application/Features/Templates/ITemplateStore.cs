using Ardalis.Result;
using VeriShip.Application.Features.Templates.Commands;
using VeriShip.Application.Features.Templates.Queries;
using VeriShip.Domain.Templates;

namespace VeriShip.Application.Features.Templates;

public interface ITemplateStore
{
    Task<Result<IEnumerable<Template>>> Query(Get request, CancellationToken cancellationToken = default);
    Task<Result<int>> Handle(Upsert Command, CancellationToken cancellationToken = default);
    Task<Result<int>> Handle(Remove Command, CancellationToken cancellationToken = default);
}