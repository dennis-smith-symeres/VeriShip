using VeriShip.Domain.Common;

namespace VeriShip.Domain.Templates;

public class Template : Entity
{
    public string Name { get; set; }
    public TemplateType TemplateType { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Size { get; set; }
}