namespace VeriShip.Domain.Entities.QcRequests.Items;

public class ItemResult : QCSpecifications.Result
{
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
}