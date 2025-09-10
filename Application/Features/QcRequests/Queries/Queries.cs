using System.Security.Claims;
using VeriShip.Application.Common;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Application.Features.Signals.Queries;

namespace VeriShip.Application.Features.QcRequests.Queries;

public class GetRequests(string projectNumber, ClaimsPrincipal authStateUser) : GetProject(projectNumber, authStateUser)
{
    
}

public class GetRequest(string projectNumber, ClaimsPrincipal authStateUser, int requestId) : GetProject(projectNumber, authStateUser)
{
    public int RequestId { get; } = requestId;
}