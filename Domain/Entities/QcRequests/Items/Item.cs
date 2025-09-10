using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.QcRequests.Items;

public class Item: Entity
{
    public int RequestId { get; set; }
    public virtual QcRequest QcRequest { get; set; }

    public Status Status { get; set; } = Status.Draft;
    public string Comment { get; set; }
    public string StructureAsSvg { get; set; }
    public string StructureAsMol { get; set; }
    public string? StructureSalts { get; set; }
    
    
    public virtual List<ItemResult> CheckResults { get; set; } = [];

    public List<int> AttachmentIds { get; set; } = [];
    public virtual List<global::VeriShip.Domain.Entities.Boxes.Items.Item> BoxItems { get; set; } = [];

}