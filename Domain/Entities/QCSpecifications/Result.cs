using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.QCSpecifications;

public class Result : Entity
{
    public string? Value { get; set; }
    public string? Acceptance { get; set; }
    public bool IsDefault { get; set; }
        
    public int CheckId { get; set; }
    public virtual QcSpecification QcSpecification { get; set; }
}