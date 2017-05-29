using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class PersonTimetable : IEntity
    {
        public Guid Id { get; set; }
        public Guid TimetableId { get; set; }
        public Timetable Timetable { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}