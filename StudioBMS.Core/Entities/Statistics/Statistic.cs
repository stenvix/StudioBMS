using System.Collections.Generic;

namespace StudioBMS.Core.Entities.Statistics
{
    public class Statistic
    {
        public Statistic()
        {
            BarStatistics = new List<BarStatistic>();
            AvarageBills = new List<AvarageBillStatistic>();
        }
        public List<BarStatistic> BarStatistics { get; set; }
        public PieStatistic PieStatistic { get; set; }
        public List<AvarageBillStatistic> AvarageBills { get; set; }

    }
}