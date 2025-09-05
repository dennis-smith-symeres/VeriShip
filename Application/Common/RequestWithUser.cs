using System.Security.Claims;

namespace VeriShip.Application.Common;

public abstract record RequestWithUser(ClaimsPrincipal User);
