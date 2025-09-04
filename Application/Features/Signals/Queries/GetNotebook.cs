using System.ComponentModel.DataAnnotations;

namespace VeriShip.Application.Features.Signals.Queries;

public class GetNotebook
{
    public GetNotebook()
    {
        
    }
    public GetNotebook(string projectNumber, string user)
    {
        ProjectNumber = projectNumber;
        User = user;
    }
    [Required]
    public string User { get; set; }
    
    [Required(ErrorMessage = "Project number is required")]
    [RegularExpression("^\\d{5,8}$", ErrorMessage = "Incorrect project number")]
    [Display(Name = "Project number")]
    public string ProjectNumber { get; set; }
}