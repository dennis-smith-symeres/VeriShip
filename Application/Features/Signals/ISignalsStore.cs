using Ardalis.Result;
using VeriShip.Application.Features.Signals.Models;

namespace VeriShip.Application.Features.Signals;

public interface ISignalsStore
{
    Task<Result<Notebook>> Query(GetNotebook request);
}