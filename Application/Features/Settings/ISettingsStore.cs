using Ardalis.Result;
using VeriShip.Application.Features.Settings.Queries;
using VeriShip.Domain.Entities.Settings;

namespace VeriShip.Application.Features.Settings;

public interface ISettingsStore
{
    Task<Result<Setting>> Query(GetSettings request, CancellationToken cancellationToken = default);
}