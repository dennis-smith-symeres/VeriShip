using VeriShip.Application.Features.Signals.Models;

namespace VeriShip.Application.Features.Signals;

public interface ISignalsStore
{
    Task<Notebook?> Query(GetNotebook request);
}