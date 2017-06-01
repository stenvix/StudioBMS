using System;
using System.Collections.Generic;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public int OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Person Customer { get; set; }
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public Guid PerformerId { get; set; }
        public Person Performer { get; set; }
        public IEnumerable<OrderService> OrderServices { get; set; }
        public DateTime Date { get; set; }
        public long Price { get; set; }
        public bool IsPaid { get; set; }
    }
}