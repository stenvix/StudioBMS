using System;
using System.Collections.Generic;
using System.Text;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class WorkingHour: IEntity
    {
        public Guid Id { get; set; }
        public byte WeekDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
