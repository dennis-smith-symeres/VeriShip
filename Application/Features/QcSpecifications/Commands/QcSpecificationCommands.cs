using System.Security.Claims;
using VeriShip.Application.Common;
using VeriShip.Domain.Entities.QCSpecifications;


namespace VeriShip.Application.Features.QcSpecifications.Commands;
/// <summary>
/// Command to sort records in a specified table.
/// </summary>
public record Sort(ClaimsPrincipal ClaimsPrincipal, Table Table, int[] Order) : Command(ClaimsPrincipal);



/// <summary>
/// Command to add or update a QcSpecification.
/// </summary>
public record Upsert(ClaimsPrincipal ClaimsPrincipal, QcSpecification Specification) : Command(ClaimsPrincipal);
