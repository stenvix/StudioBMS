using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
    }
}