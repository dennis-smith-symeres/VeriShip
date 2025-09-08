using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.Boxes.Items
{
    public class Item : Entity
    {
        public string Name { get; set; }

        public string? Barcode { get; set; }
        public string? StructureAsMol { get; set; }
        public string? StructureAsSvg { get; set; }
        public double? Amount { get; set; }
        public double? Quantity { get; set; }
        public Unit? Unit { get; set; }
        public string? Purity { get; set; }
        public int BoxId { get; set; }
        public virtual Box Box { get; set; }

        public int? RequestItemId { get; set; }
        public virtual VeriShip.Domain.Entities.QcRequests.Items.Item? RequestItem { get; set; }
        public List<ExtraField>? ExtraFields { get; set; } = new();
        public string? Mw { get; set; }
    }
}