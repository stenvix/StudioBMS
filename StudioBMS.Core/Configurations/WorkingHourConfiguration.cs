using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    public class WorkingHourConfiguration : EntityMappingConfiguration<WorkingHour>
    {
        public override void Map(EntityTypeBuilder<WorkingHour> b)
        {
            b.HasKey(i => i.Id);
            b.Property(i => i.Start).HasColumnType("datetime2");
            b.Property(i => i.End).HasColumnType("datetime2");
        }
    }
}