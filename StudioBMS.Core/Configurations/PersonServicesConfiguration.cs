using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class PersonServicesConfiguration : EntityMappingConfiguration<PersonService>
    {
        public override void Map(EntityTypeBuilder<PersonService> b)
        {
            b.HasKey(i => new { i.ServiceId, i.PersonId });
            b.HasOne(i => i.Person).WithMany(i => i.PersonServices).HasForeignKey(i => i.PersonId);
            b.HasOne(i => i.Service).WithMany(i => i.PersonServices).HasForeignKey(i => i.ServiceId);
        }
    }
}