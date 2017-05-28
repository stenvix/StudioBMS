using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class ItemTimeTableConfiguration : EntityMappingConfiguration<ItemTimeTable>
    {
        public override void Map(EntityTypeBuilder<ItemTimeTable> b)
        {
            b.HasKey(i => new {i.TimeTableId, i.WorkshopId});
            b.HasOne(i => i.Workshop).WithMany(i => i.ItemTimeTables).HasForeignKey(i => i.WorkshopId);
            b.HasOne(i => i.TimeTable).WithMany(i => i.ItemTimeTables).HasForeignKey(i => i.TimeTableId);
        }
    }
}