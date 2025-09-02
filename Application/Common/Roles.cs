using System.ComponentModel;

namespace VeriShip.Application.Common;


[DisplayName("Role Permissions")]
[Description("Set permissions for role operations")]
public static class Roles
{
    [Description("Allows access to application")]
    public const string Access = "QC.Access";

    [Description("Allows access to all application operations")]
    public const string Admin = "QC.Admin";
    
    [Description("Allows access to settings")]
    public const string Officer = "QC.Officer";
}

