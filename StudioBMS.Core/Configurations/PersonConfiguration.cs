﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class PersonConfiguration : EntityMappingConfiguration<Person>
    {
        public override void Map(EntityTypeBuilder<Person> b)
        {
            b.HasKey(i => i.Id);
            b.Property(i => i.Birthday).HasColumnType("datetime2");
            b.Property(i => i.FirstName).IsRequired();
            b.Property(i => i.LastName).IsRequired();
        }
    }
}