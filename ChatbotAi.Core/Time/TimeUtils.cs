namespace ChatbotAi.Core.Time;

internal static class TimeUtils
{
    public static DateTime GetDate(this TimeProvider timeProvider)
    => timeProvider.GetUtcNow().Date;
}
