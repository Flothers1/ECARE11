namespace ECARE.Constants
{
    public static class SD
    {
        public static DateTime TimeInEgypt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
               TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time"));

    }
}
