using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.Settings;

public class Setting: Entity
{
    public List<int> AttachmentIdsLabels { get; set; } = [];
    public List<int> AttachmentIdsAddressLabels { get; set; } = [];
    public List<int> AttachmentIdsPackingLists { get; set; } = [];
    public int AttachmentIdCoA { get; set; }
}