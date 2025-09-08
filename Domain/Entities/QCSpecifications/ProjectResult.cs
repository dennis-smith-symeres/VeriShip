
using VeriShip.Domain.Entities.Projects;

namespace VeriShip.Domain.Entities.QCSpecifications;

public class ProjectResult : Result
{
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }

    
}