using Humanizer;
using Telerik.Blazor.Components;

namespace VeriShip.WebApp.Services;

public class Notifications : INotifications
{
    
        public TelerikNotification? NotificationRef { get; set; }

        public void Show(string text, string? themeColor = null)
        {
            NotificationRef?.Show(new NotificationModel()
            {
                ShowIcon = false,
                Text = text.Humanize(),
                ThemeColor = themeColor
            });
        }

        public void Show(NotificationModel notificationModel)
        {
            notificationModel.Text = notificationModel.Text.Humanize();
            notificationModel.ShowIcon = false;
            NotificationRef?.Show(notificationModel);
        }

        public void HideAll()
        {
            NotificationRef?.HideAll();
        }
    
}