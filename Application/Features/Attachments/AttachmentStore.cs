using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VeriShip.Application.Features.Attachments.Queries;
using VeriShip.Domain.Entities.Attachments;
using VeriShip.Infrastructure.Persistence;

namespace VeriShip.Application.Features.Attachments;

public class AttachmentStore(IApplicationDbContextFactory dbContextFactory, ILogger<AttachmentStore> logger) : IAttachmentStore
{

    public async Task<Result<IEnumerable<Attachment>>> Query(GetByIds request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var db = await dbContextFactory.CreateAsync(cancellationToken);
            var attachments = await db.Attachments.AsNoTracking()
                .Where(x => request.ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
            return Result<IEnumerable<Attachment>>.Success(attachments);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting notebook");
            return Result<IEnumerable<Attachment>>.Error(e.Message);
        }
    }
}