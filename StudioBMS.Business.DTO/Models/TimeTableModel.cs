using System;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class TimeTableModel : IModel
    {
        public DayOfWeek WeekDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid Id { get; set; }
    }
}