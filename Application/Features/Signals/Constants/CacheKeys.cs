namespace VeriShip.Application.Features.Signals.Constants;

public static class CacheKeys
{
    private const string SignalsCacheKey = "Signals";
    
    private const string UserPrefix = "User";
    private const string JournalPrefix = "journal";
    
    
    public static string User(string userId) => $"{SignalsCacheKey}:{UserPrefix}:{userId}";
    public static string Journal(string journalId) => $"{SignalsCacheKey}:{JournalPrefix}:{journalId}";
}