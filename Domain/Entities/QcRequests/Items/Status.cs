namespace VeriShip.Domain.Entities.QcRequests.Items;

public enum Status
{
  
    Draft = 0,
  
    InternalCheck = 1,

    QCCheck= 2,
  
    Accepted = 3,
}

public static class StatusExtensions
{
    public static string ToFriendlyString(this Status status) =>
        status switch
        {
            Status.InternalCheck => "Primary check",
            Status.QCCheck => "QC check",
            _ => status.ToString()
        };
}