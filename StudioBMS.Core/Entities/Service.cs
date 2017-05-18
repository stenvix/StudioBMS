using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Service : IEntity
    {
        public Guid Id { get; set; }
    }
}