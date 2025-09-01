using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.QCSpecifications;

public class Field : Entity
{
    public bool IsDefault { get; set; }
    public string? Comment { get; set; }
    public bool AllowCustomValue { get; set; }
    public bool AllowCustomAcceptance { get; set; }
    public List<string> Values { get; set; } = [];

    public SpecialField SpecialField { get; set; } = SpecialField.None;

}