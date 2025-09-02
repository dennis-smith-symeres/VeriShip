using VeriShip.Application.Common;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features.QcSpecifications.Commands;

public record Upsert(string User, QcSpecification Specification) : Command(User);