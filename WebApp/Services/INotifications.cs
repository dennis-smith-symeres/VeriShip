using Telerik.Blazor.Components;

namespace VeriShip.WebApp.Services;

public interface INotifications
{
    TelerikNotification? NotificationRef { get; set; }
    void Show(string text, string? themeColor = null);
    void Show(NotificationModel notificationModel);
    void HideAll();
}