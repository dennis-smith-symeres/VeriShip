namespace VeriShip.Domain.Entities.Email;

public class EmailMessage
{
    public int Id { get; set; }

    public string SenderEmail { get; set; } = null!;

    public  List<string> RecipientEmails { get; set; } = []; 

    public List<string> CcEmails { get; set; } = []; 

    public List<string> BccEmails { get; set; } = []; 

    public string? Subject { get; set; } 

    public string? Body { get; set; } 

    public bool IsHtml { get; set; } = false;

    public string? Attachments { get; set; } 

    public Status Status { get; set; } = Status.Pending; // Default value

    public int RetryCount { get; set; } = 0; 

    public DateTime CreatedOn { get; set; } = DateTime.Now; 

    public DateTime? SentDate { get; set; } 

    public string? ErrorMessage { get; set; } 

    public string CreatedBy { get; set; } = null!; 
}
