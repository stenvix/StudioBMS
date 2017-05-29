using System;
using System.Collections.Generic;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Workshop : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<WorkshopTimetable> WorkshopTimetables{ get; set; }
        public IEnumerable<Person> Persons { get; set; }
    }
}