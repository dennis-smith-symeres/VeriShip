

using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.Projects;
using VeriShip.Domain.Entities.QcRequests.Items;

namespace VeriShip.Domain.Entities.QcRequests;

public class QcRequest : Entity
{
    
 
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }

    public string Comment { get; set; }
    public string Label { get; set; }
    public List<int> AttachmentIds { get; set; } = [];
    public virtual List<Item> Items { get; set; } = [];

}