using System;
using System.Collections.Generic;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Core.Entities
{
    public class Service : IEntity
    {
        public Guid Id { get; set; }
        public string EnTitle { get; set; }
        public string RuTitle { get; set; }
        public string UkTitle { get; set; }
        public DateTime Duration { get; set; }
        public int Price { get; set; }
        public IEnumerable<PersonService> PersonServices { get; set; }
        public bool IsActive { get; set; }
    }
}