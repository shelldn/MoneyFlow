using System;

namespace MoneyFlow.Model
{
    public partial class Cost
    {
        public static Cost At(DateTime date)
        {
            return new Cost { Date = date };
        }
    }
}