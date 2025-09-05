using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace VeriShip.Application.Features.Signals.Queries;

public class GetNotebook
{
    public GetNotebook()
    {
        
    }
    public GetNotebook(string projectNumber, ClaimsPrincipal claimsPrincipal)
    {
        ProjectNumber = projectNumber;
        ClaimsPrincipal = claimsPrincipal;
    }
    [Required]
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
    
    [Required(ErrorMessage = "Project number is required")]
    [RegularExpression("^\\d{5,8}$", ErrorMessage = "Incorrect project number")]
    [Display(Name = "Project number")]
    public string ProjectNumber { get; set; }
}