using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features.QcSpecifications.Queries;

public record GetAll()
{
    public Table Table { get; set; } 
}