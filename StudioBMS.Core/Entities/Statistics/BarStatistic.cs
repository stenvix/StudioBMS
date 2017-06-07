using System.Collections.Generic;

namespace StudioBMS.Core.Entities.Statistics
{
    public class BarStatistic
    {
        public string Label { get; set; }
        public IList<BarStatisticOrderItem> OrderItems { get; set; }
        public IList<BarStatisticPaymentItem> PaymentItems { get; set; }
    }
}