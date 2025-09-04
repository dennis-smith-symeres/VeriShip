using Ardalis.Result;
using VeriShip.Application.Features.Signals.Models;
using VeriShip.Application.Features.Signals.Queries;

namespace VeriShip.Application.Features.Signals;

public interface ISignalsStore
{
    Task<Result<Notebook>> Query(GetNotebook request);
}