namespace VeriShip.Application.Features.Signals.Models;

public class Notebook
{
    public string Description { get; set; }
    public string Id { get; set; }
    public bool CanRead { get; set; }
    public string? Client { get; set; }
    public bool CanWrite { get; set; }
}