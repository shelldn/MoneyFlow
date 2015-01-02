using System;
using MoneyFlow.Model;

namespace MoneyFlow.Test
{
    internal static class DateTimeExtensions
    {
        public static Cost Spend(this DateTime dt)
        {
            return new Cost { Date = dt };
        }
    }
}