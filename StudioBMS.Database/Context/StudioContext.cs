using System;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Core.Configurations.Base.Implementations;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Database.Context
{
    public class StudioContext : IdentityDbContext<Person, Role, Guid, PersonClaim, PersonRole, PersonLogin, RoleClaim,
        PersonToken>
    {
        public StudioContext(DbContextOptions<StudioContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<PersonClaim>().ToTable("PersonClaims");
            builder.Entity<PersonLogin>().ToTable("PersonLogins");
            builder.Entity<PersonRole>().ToTable("PersonRoles");
            builder.Entity<PersonToken>().ToTable("PersonTokens");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<Timetable>().ToTable("Timetables");
            builder.Entity<PersonRole>().HasOne(i => i.Person).WithMany(i => i.Roles).HasForeignKey(i=>i.UserId);
            builder.Entity<PersonRole>().HasOne(i => i.Role).WithMany(i => i.Users).HasForeignKey(i=>i.RoleId);
            builder.AddEntityConfigurationsFromAssembly(typeof(IEntity).GetTypeInfo().Assembly);
        }

        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<WorkshopTimetable> WorkshopTimetables { get; set; }
        public DbSet<Timetable> TimeTables { get; set; }
        public DbSet<PersonTimetable> PersonTimetables { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}