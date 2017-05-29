using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class WorkshopTimetable : IEntity
    {
        public Guid Id { get; set; }
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public Guid TimetableId { get; set; }
        public Timetable Timetable { get; set; }
    }
}