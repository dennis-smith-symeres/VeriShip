using System.ComponentModel.DataAnnotations;

namespace VeriShip.Domain.Entities.Boxes;

public class BoxType
{
    
    public int Id { get; set; }

    [Required]
    public bool Active { get; set; }

    [Required]
    public string CreatedBy { get; set; }

    
    public DateTime? CreatedOn { get; set; }

    [Required]
    [MaxLength(255)]
    public string Conditions { get; set; }

    [Required]
    [MaxLength(255)]
    public string Type { get; set; }

    [Required]
    [MaxLength(255)]
    public string Label { get; set; }
    
    [MaxLength(255)]
    public string? Dimensions_LxWxH { get; set; }
    
    public int? DeclaredWeight_Kg { get; set; }

    [Required]
    public bool DryIce { get; set; }

    [Required]
    public bool ColdPacks { get; set; }

    [Required]
    public bool TemperatureLogger { get; set; }

    public virtual Box Box { get; set; }

}