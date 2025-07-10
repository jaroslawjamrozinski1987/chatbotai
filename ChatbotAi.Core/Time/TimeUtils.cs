namespace ChatbotAi.Core.Time;

internal static class TimeUtils
{
    public static DateTime GetDate(this TimeProvider timeProvider)
    {
        var utcNow = timeProvider.GetUtcNow().UtcDateTime;
        return DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
    }
}
