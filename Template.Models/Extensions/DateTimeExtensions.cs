namespace Template.Models.Extensions;

public static class DateTimeExtensions
{
    public static DateTime GetStartOfWeek(this DateTime dateTime)
    {
        DateTime startOfWeek = dateTime;
        while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
            startOfWeek = startOfWeek.AddDays(-1);

        return startOfWeek.Date;
    }

    public static DateTime GetEndOfWeek(this DateTime startOfWeek) => startOfWeek.AddDays(5);
    public static DateTime AddWeek(this DateTime dateTime) => dateTime.AddDays(7);
}