using System.Collections.Generic;

namespace StudioBMS.Core.Entities.Statistics
{
    public class BarStatistic
    {
        public string Label { get; set; }
        public BarStatisticOrderItem OrderItems { get; set; }
        public BarStatisticPaymentItem PaymentItems { get; set; }
    }
}