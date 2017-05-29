using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations
{
    internal class OrderConfiguration : EntityMappingConfiguration<Order>
    {
        public override void Map(EntityTypeBuilder<Order> b)
        {
            b.HasKey(i => i.Id);
            b.HasOne(i => i.Workshop).WithMany().HasForeignKey(i=>i.WorkshopId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(i => i.Customer).WithMany().HasForeignKey(i=>i.CustomerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(i => i.Performer).WithMany().HasForeignKey(i=>i.PerformerId).OnDelete(DeleteBehavior.Restrict);
            b.Property(i => i.Price).IsRequired();
        }
    }
}