using VeriShip.Application.Common;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features.QcSpecifications.Commands;
/// <summary>
/// Command to sort records in a specified table.
/// </summary>
public record Sort(string User, Table Table, int[] Order) : Command(User);



/// <summary>
/// Command to add or update a QcSpecification.
/// </summary>
public record Upsert(string User, QcSpecification Specification) : Command(User);
