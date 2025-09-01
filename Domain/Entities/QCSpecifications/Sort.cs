using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.QCSpecifications;


public class Sort : Entity
{
    
    public Table Table { get; set; }
    public int[] Order { get; set; } = Array.Empty<int>();
}