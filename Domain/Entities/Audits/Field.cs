namespace VeriShip.Domain.Entities.Audits;

public class Field
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Value { get; set; }
    public int AuditId { get; set; }
    public virtual Audit Audit { get; set; }
    public bool IsPrimitiveCollection { get; set; } = false;
}

