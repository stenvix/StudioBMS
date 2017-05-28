using System;
using System.ComponentModel.DataAnnotations;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class TimeTableModel : IModel
    {
        public Guid Id { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}