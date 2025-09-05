using System.Security.Claims;
using Ardalis.Result;

namespace VeriShip.Application.Common;

public abstract record Command(ClaimsPrincipal ClaimsPrincipal);
