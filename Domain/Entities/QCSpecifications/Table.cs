using System.ComponentModel.DataAnnotations;

namespace VeriShip.Domain.Entities.QCSpecifications;

public enum Table
{
    [Display(Name = "General Information")]
    GeneralInformation = 1,
    
    [Display(Name = "Tests")]
    Tests = 2,
    
    [Display(Name = "Metadata")]
    Metadata = 3,
}

