namespace VeriShip.Domain.Entities.Audits;

public partial class Audit 
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string? TableName { get; set; }

    public DateTime DateTime { get; set; } = DateTime.UtcNow;

    public int? RowId { get; set; }
    
    public virtual List<Field> NewValues { get; set; } = new();
    
    public required string Action { get; set; }
}