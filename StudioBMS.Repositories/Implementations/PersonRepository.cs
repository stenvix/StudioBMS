using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;
using StudioBMS.Repositories.Implementations.Base;
using StudioBMS.Repositories.Interfaces;

namespace StudioBMS.Repositories.Implementations
{
    public class PersonRepository :Repository<Person>, IPersonRepository
    {
        public PersonRepository(StudioContext context) : base(context)
        {
        }

        protected override IEnumerable<Person> Include()
        {
            return Set.Include(i => i.Roles).ThenInclude(i => i.Role);
        }
    }
}