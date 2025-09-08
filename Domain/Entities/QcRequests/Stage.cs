using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.QcRequests.Items;

namespace VeriShip.Domain.Entities.QcRequests;

public class Stage : Entity
{
    public int QcRequestId { get; set; }
    public virtual QcRequest QcRequest { get; set; }
    public string Comment { get; set; }
    public Status NextStatusId { get; set; }
    public Status PreviousStatusId { get; set; }
  
}