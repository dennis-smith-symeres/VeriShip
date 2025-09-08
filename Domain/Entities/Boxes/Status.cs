namespace VeriShip.Domain.Entities.Boxes;

public enum Status
{
    Draft = 0,
    PrimaryCheck = 1,
    SecondaryCheck = 2,
    Closed = 3,
    Send = 4
}
public static class StatusExtensions
{
    public static string ToFriendlyString(this Status status) =>
        status switch
        {
            Status.PrimaryCheck => "Primary check",
            Status.SecondaryCheck => "Secondary check",
            _ => status.ToString()
        };
}