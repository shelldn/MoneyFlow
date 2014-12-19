using System;

namespace MoneyFlow.Data
{
    public static class DateTimeExtensions
    {
        public static bool MonthEquals(this DateTime a, DateTime b)
        {
            return 
                a.Month == b.Month &&
                a.Year == b.Year;
        }
    }
}