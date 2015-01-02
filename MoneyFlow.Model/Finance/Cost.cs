using System;

namespace MoneyFlow.Model
{
    public partial class Cost
    {
        public static Cost At(int year, int month, int day)
        {
            return At(new DateTime(year, month, day));
        }

        public static Cost At(DateTime date)
        {
            return new Cost { Date = date };
        }

        public static Cost At(string date)
        {
            return At(DateTime.Parse(date));
        }
    }
}