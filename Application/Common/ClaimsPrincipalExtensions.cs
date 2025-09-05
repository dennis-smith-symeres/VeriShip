using System.Security.Claims;
using Ardalis.Result;

namespace VeriShip.Application.Common;

public static class ClaimsPrincipalExtensions
{
    public static Result<AuthenticatedUser> ToUserResult(this ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity;

        var claims = claimsPrincipal.Claims.ToArray();
        if (string.IsNullOrEmpty(identity?.Name))
        {
            return Result<AuthenticatedUser>.Unauthorized();
        }
        var user = new AuthenticatedUser()
        {
            Name = identity.Name,
            DisplayName = claims.FirstOrDefault(c => c.Type == "name")?.Value,
            IsAdmin = claims.Any(c => c is { Type: ClaimTypes.Role, Value: Roles.Admin }),
            IsQc = claims.Any(c => c is { Type: ClaimTypes.Role, Value: Roles.Officer }),
            HasAccess = claims.Any(c => c is { Type: ClaimTypes.Role, Value: Roles.Access })
        };
        return user switch
        {
            { Name: null or "" } => Result<AuthenticatedUser>.Unauthorized(),
            { HasAccess: false } => Result<AuthenticatedUser>.Forbidden(),
            _ => Result.Success(user)
        };
    }
}