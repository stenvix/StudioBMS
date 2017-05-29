using System;
using System.Collections.Generic;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class OrderStatus : IEntity
    {
        public const string Active = "Active";
        public const string Declined = "Declined";
        public const string Done = "Done";

        public Guid Id { get; set; }
        public string Name { get; set; }
        private IEnumerable<Order> Orders { get; set; }
    }
}