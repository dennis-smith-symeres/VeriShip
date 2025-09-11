using Ardalis.Result;
using VeriShip.Application.Features.Exports.Queries;

namespace VeriShip.Application.Features.Exports;

public interface IExportStore
{
    Task<Result<byte[]>> Handle(PreviewCoA request, CancellationToken cancellationToken = default);
}