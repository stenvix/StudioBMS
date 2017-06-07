namespace StudioBMS.Core.Entities.Statistics
{
    public class BarStatisticOrderItem : BarStatisticItem
    {
        public double Done { get; set; }
        public double Declined { get; set; }
        public double Active { get; set; }
    }
}