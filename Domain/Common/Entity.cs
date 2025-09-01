namespace VeriShip.Domain.Common;

public abstract class Entity
{
    public int Id { get; set; }
    public bool Active { get; set; } = true;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
}