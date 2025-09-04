using VeriShip.Application.Common;

namespace VeriShip.Application.Features.QcRequests.Queries;

public record GetRequests(string User, string projectNumber) : RequestWithName(User);