using Ardalis.Result;
using VeriShip.Application.Features.QcRequests.Queries;
using VeriShip.Domain.Entities.QcRequests;

namespace VeriShip.Application.Features.QcRequests;

public interface IQcRequestStore
{
    Task<Result<IEnumerable<QcRequest>>> Query(GetRequests request, CancellationToken cancellationToken = default);
    Task<Result<QcRequest>> Query(GetRequest request, CancellationToken cancellationToken = default);
}