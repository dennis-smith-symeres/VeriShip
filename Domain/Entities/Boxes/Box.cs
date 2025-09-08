using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.Addresses;
using VeriShip.Domain.Entities.Boxes.Items;
using VeriShip.Domain.Entities.Projects;

namespace VeriShip.Domain.Entities.Boxes;

public class Box: Entity
{
  
    public Status Status { get; set; } = Status.Draft;
    public string Label { get; set; }
    
    public List<int> AttachmentIds { get; set; } = [];
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
    public virtual List<Item> Items { get; set; } = [];    
    public virtual List<Stage> Stages { get; set; } = [];
    public int? BoxWeight { get; set; }
    public string? BoxDimensions { get; set; }

    public bool? DryIce { get; set; }


    public bool? ColdPacks { get; set; }
    
    public bool? TemperatureLogger { get; set; }
    
    public int? BoxTypeId { get; set; }
    public virtual BoxType? BoxType { get; set; }

    public int? AddressId { get; set; }
    public virtual Address Address { get; set; }
}