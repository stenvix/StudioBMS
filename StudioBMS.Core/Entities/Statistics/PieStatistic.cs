using System;
using System.Collections.Generic;
using System.Text;

namespace StudioBMS.Core.Entities.Statistics
{
    public class PieStatistic
    {
        public long Done { get; set; }
        public long Active { get; set; }
        public long Declined { get; set; }
    }
}
