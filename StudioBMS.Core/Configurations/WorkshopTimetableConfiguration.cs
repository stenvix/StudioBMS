using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class WorkshopTimetableConfiguration : EntityMappingConfiguration<WorkshopTimetable>
    {
        public override void Map(EntityTypeBuilder<WorkshopTimetable> b)
        {
            b.HasKey(i => new {TimeTableId = i.TimetableId, i.WorkshopId});
            b.HasOne(i => i.Workshop).WithMany(i => i.WorkshopTimetables).HasForeignKey(i => i.WorkshopId);
            b.HasOne(i => i.Timetable).WithMany(i => i.WorkshopTimetables).HasForeignKey(i => i.TimetableId);
        }
    }
}