using Ardalis.Result;
using VeriShip.Application.Features.QcRequests.Queries;
using VeriShip.Domain.Entities.QcRequests;

namespace VeriShip.Application.Features.QcRequests;

public interface IQcRequestStore
{
    Task<Result<IEnumerable<QcRequest>>> Query(GetRequests request);
}