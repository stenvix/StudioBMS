using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class ServiceConfiguration : EntityMappingConfiguration<Service>
    {
        public override void Map(EntityTypeBuilder<Service> b)
        {
            b.HasKey(i => i.Id);
            b.Property(i => i.EnName).IsRequired();
            b.Property(i => i.RuName).IsRequired();
            b.Property(i => i.UkName).IsRequired();
        }
    }
}