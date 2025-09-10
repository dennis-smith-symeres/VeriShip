using System.Collections;
using System.Security.Claims;
using Ardalis.Result;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Application.Features.Signals.Queries;
using VeriShip.Domain.Entities.QCSpecifications;
using Result = VeriShip.Domain.Entities.QCSpecifications.Result;

namespace VeriShip.Application.Features.Projects.Commands;



public class SaveDefaultQcSpecifications(string projectNumber, ClaimsPrincipal claimsPrincipal, IEnumerable<Result> qcResults) : GetProject(projectNumber, claimsPrincipal)
{
    public IEnumerable<Result> QCResults { get; set; } = qcResults;
}