using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class PersonService : IEntity
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}