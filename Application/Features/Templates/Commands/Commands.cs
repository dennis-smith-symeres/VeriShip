using System.Security.Claims;
using VeriShip.Domain.Templates;

namespace VeriShip.Application.Features.Templates.Commands;

public record Upsert
{
    public TemplateType TemplateType { get; set; }
    public string FileName { get; init; }
    public byte[] Bytes { get; init; }
    public ClaimsPrincipal User { get; init; }
}

public record Remove(int Id, ClaimsPrincipal User);