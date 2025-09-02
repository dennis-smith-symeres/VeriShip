using VeriShip.Application.Common;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features.QcSpecifications.Commands;

public record Sort(string User, Table Table, int[] Order) : Command(User);