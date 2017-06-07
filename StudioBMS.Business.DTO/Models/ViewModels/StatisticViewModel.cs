using System;

namespace StudioBMS.Business.DTO.Models.ViewModels
{
    public class StatisticViewModel
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public Guid[] Ids { get; set; }
    }
}