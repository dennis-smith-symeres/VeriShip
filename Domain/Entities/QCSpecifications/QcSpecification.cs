

using System.ComponentModel.DataAnnotations;

namespace VeriShip.Domain.Entities.QCSpecifications;

public class QcSpecification : Field, IValidatableObject
{
 
    [Required]
    public string Category { get; set; }

    public string? Technique { get; set; }

    [Required]
    public string Acceptance { get; set; }

    [Required]
    public Table Table { get; set; }

    

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Table == Table.Tests && string.IsNullOrEmpty(Technique))
        {
            yield return new ValidationResult(
                $"The {nameof(Technique)} field is required.",
                [nameof(Technique)]);
        }
    }
}