using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VeriShip.WebApp.Services;

public static class Attributes
{
    public static string GetDisplayName<T>(string propertyName)
    {
        var prop = typeof(T).GetProperty(propertyName);
        if (prop != null)
        {
            var displayNameAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttr != null)
            {
                return displayNameAttr.DisplayName;
            }
        }
        return propertyName;
    }
    
    public static string GetEnumDisplayName(Enum enumValue)
    {
        var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
        if (memberInfo.Length > 0)
        {
            var displayAttr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
            {
                return displayAttr.Name ?? string.Empty;
            }
        }
        // Fallback to enum value name
        return enumValue.ToString();
    }
}