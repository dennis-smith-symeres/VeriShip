using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features.QcSpecifications;

public record GetAllRequest()
{
    public Table Table { get; set; } 
}