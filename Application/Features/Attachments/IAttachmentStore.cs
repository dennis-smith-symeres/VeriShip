using Ardalis.Result;
using VeriShip.Application.Features.Attachments.Queries;
using VeriShip.Domain.Entities.Attachments;

namespace VeriShip.Application.Features.Attachments;

public interface IAttachmentStore
{
    Task<Result<IEnumerable<Attachment>>> Query(GetByIds request,
        CancellationToken cancellationToken = default);
}