using System;
using System.Collections.Generic;
using System.Text;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class OrderService: IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
