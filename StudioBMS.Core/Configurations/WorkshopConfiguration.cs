using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class WorkshopConfiguration : EntityMappingConfiguration<Workshop>
    {
        public override void Map(EntityTypeBuilder<Workshop> b)
        {
            b.HasKey(i => i.Id);
        }
    }
}