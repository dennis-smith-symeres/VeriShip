using System.Collections.Concurrent;

namespace VeriShip.WebApp.Services
{
    public interface IUsersStateContainer
    {
        ConcurrentDictionary<string, string> UsersByConnectionId { get; set; }
        event Action OnChange;
        void Update(string connectionId, string name);
        void Remove(string connectionId);
    }

    public class UsersStateContainer : IUsersStateContainer
    {
        public ConcurrentDictionary<string, string> UsersByConnectionId { get; set; } =
            new ConcurrentDictionary<string, string>();

        public event Action OnChange;
        public void Update(string connectionId, string name)
        {
            UsersByConnectionId.AddOrUpdate(connectionId, name, (key, oldValue) => name );
            NotifyStateChanged();
        }
        public void Remove(string connectionId)
        {
            UsersByConnectionId.TryRemove(connectionId, out var _);
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}