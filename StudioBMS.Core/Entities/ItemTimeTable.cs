using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class ItemTimeTable : IEntity
    {
        public Guid Id { get; set; }
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public Guid TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}