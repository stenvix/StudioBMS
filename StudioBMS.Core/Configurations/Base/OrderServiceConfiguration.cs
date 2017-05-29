using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;

namespace StudioBMS.Core.Configurations.Base
{
    internal class OrderServiceConfiguration : EntityMappingConfiguration<OrderService>
    {
        public override void Map(EntityTypeBuilder<OrderService> b)
        {
            b.HasKey(i => i.Id);
            b.HasOne(i => i.Order).WithMany(i => i.OrderServices).HasForeignKey(i => i.OrderId);
            b.HasOne(i => i.Service).WithMany().HasForeignKey(i => i.ServiceId);
        }
    }
}