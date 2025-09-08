using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.Boxes;

public class Stage : Entity
{
    public int BoxId { get; set; }
    public virtual Box Box { get; set; }
    public string Comment { get; set; }
    public Status NextStatusId { get; set; }
    public Status PreviousStatusId { get; set; }
  
}