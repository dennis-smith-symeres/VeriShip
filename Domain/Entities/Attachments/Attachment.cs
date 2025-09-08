using VeriShip.Domain.Common;

namespace VeriShip.Domain.Entities.Attachments;

public class Attachment : Entity
{
    public string Name { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Type { get; set; }
    public string? SignalsEid { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Size { get; set; }
}