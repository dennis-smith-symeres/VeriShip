using System.Security.Claims;
using VeriShip.Application.Features.Signals.Queries;

namespace VeriShip.Application.Features.Projects.Queries;

public class GetProject : GetNotebook
{
    public GetProject()
    {
        
    }
    public GetProject(string projectNumber, ClaimsPrincipal claimsPrincipal)
    {
        ProjectNumber = projectNumber;
        ClaimsPrincipal = claimsPrincipal;
    }
    
   
}

public class GetDefaultQcSpecifications(string projectNumber, ClaimsPrincipal claimsPrincipal) : GetProject(projectNumber, claimsPrincipal)
{
    
}