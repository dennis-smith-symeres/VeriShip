using Ardalis.Result;
using Microsoft.Extensions.Logging;
using VeriShip.Application.Features.QcRequests.Queries;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Application.Features.QcRequests;

public class QcRequestStore(IApplicationDbContextFactory dbContextFactory, ILogger<QcRequestStore> logger)
{
    // public async Task<Result<IEnumerable<QcSpecificationResutl>>> Query(GetRequests request,
    //     CancellationToken cancellationToken)
    // {
    //     
    // }
}