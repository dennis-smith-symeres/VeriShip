using System.ComponentModel.DataAnnotations;

namespace VeriShip.Application.Features.Projects.Queries;

public class GetProject
{
    [Required]
    public required string User { get; set; }
    
    [Required(ErrorMessage = "Project number is required")]
    [RegularExpression("^\\d{5,8}$", ErrorMessage = "Incorrect project number")]
    [Display(Name = "Project number")]
    public required string ProjectNumber { get; set; }
}