namespace VeriShip.Application.Common;

public record AuthenticatedUser
{
   
    public string Name { get; set; }
    public string? DisplayName { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsQc { get; set; }
    public bool HasAccess { get; set; }
    
 
}
