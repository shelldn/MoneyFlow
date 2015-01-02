using System;

namespace MoneyFlow.Web
{
    public static class DateTimeExtensions
    {
        public static DateTime GetMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 01);
        }
    }
}