using System;
using System.Collections.Generic;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class TimeTable : IEntity
    {
        public Guid Id { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IEnumerable<ItemTimeTable> ItemTimeTables { get; set; }
    }
}