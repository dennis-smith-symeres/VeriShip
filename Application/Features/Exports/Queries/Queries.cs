using System.Security.Claims;
using VeriShip.Application.Features.Projects.Queries;
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Application.Features.Exports.Queries;

public class PreviewCoA(string ProjectNumber, ClaimsPrincipal ClaimsPrincipal) : GetProject(ProjectNumber, ClaimsPrincipal)
{
    public IEnumerable<Result> Results { get; set; } = [];
    public string Svg { get; set; }
    public string? Salts { get; set; }
};