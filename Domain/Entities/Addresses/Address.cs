using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.Boxes;
using VeriShip.Domain.Entities.Projects;

namespace VeriShip.Domain.Entities.Addresses;

public class Address : Entity
{


    public string? Description { get; set; }
    public string? ShipTo { get; set; }
    public string? ClientOrderNumber { get; set; }

    
    public string? StreetAddress { get; set; }

    public string? City { get; set; }


    public string? State { get; set; }


    public string? PostalCode { get; set; }

 
    public string? Country { get; set; }

    public string? Mail { get; set; }
    public string? Telephone { get; set; }

    public int? ProjectId { get; set; }
    public virtual Project? Project { get; set; }



    public virtual Box? Box { get; set; }
    public string? Person { get; set; }
}