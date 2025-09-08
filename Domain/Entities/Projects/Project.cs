using VeriShip.Domain.Common;
using VeriShip.Domain.Entities.Addresses;
using VeriShip.Domain.Entities.Boxes;
using VeriShip.Domain.Entities.QcRequests;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Domain.Entities.Projects;

public class Project : Entity
{
    public string ProjectNumber { get; set; }
    public List<string> InternalCheckers { get; set; } = [];
    public List<string> InternalBoxCheckers { get; set; } = [];
    public string? SignalsId { get; set; }

    public string? Client { get; set; }
    public virtual List<ProjectResult> Results { get; set; } = [];
    
    public virtual List<QcRequest> QcRequests { get; set; } = [];
    
    public virtual List<Box> Boxes { get; set; } = [];

    public virtual List<Address> Addresses { get; set; } = [];

}