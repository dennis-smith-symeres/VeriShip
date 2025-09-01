using System.ComponentModel.DataAnnotations;

namespace VeriShip.Infrastructure.Configuration;

public class DatabaseSettings
{

    public const string Key = nameof(DatabaseSettings);
    
    [Required]
    public string ConnectionString { get; set; } = string.Empty;

}