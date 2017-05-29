using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    class PersonTimetableConfiguration: EntityMappingConfiguration<PersonTimetable>
    {
        public override void Map(EntityTypeBuilder<PersonTimetable> b)
        {
            b.HasKey(i => new {i.PersonId, TimeTableId = i.TimetableId});
            b.HasOne(i => i.Person).WithMany(i => i.PersonTimetables).HasForeignKey(i => i.PersonId);
            b.HasOne(i => i.Timetable).WithMany(i => i.PersonTimetables).HasForeignKey(i => i.TimetableId);
        }
    }
}
