using System;
using System.Collections.Generic;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Database.Context
{
    public static class DatabaseInitializer
    {
        public static void Initialize(this StudioContext context)
        {
            var roles = new[]
            {
                "client",
                "manager",
                "employee",
                "administrator"
            };
            foreach (var roleName in roles)
            {
                var role = new Role {Name = roleName.ToLower(), NormalizedName = roleName.Normalize()};
                context.Roles.Add(role);
                context.SaveChanges();
            }

            //Init workshops
            var workshop = new Workshop();
            workshop.Title = "FirstPoint";
            workshop.City = "Львів";
            workshop.Address = "Лукаша 5";
            workshop.Email = "first@studio.com";
            workshop.PhoneNumber = "0999999999";
            context.Workshops.Add(workshop);
            context.SaveChanges();

            var timeTables = new List<TimeTable>
            {
                new TimeTable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Monday},
                new TimeTable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Tuesday},
                new TimeTable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Wednesday},
                new TimeTable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Thursday},
                new TimeTable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Friday}
            };

            foreach (var timeTable in timeTables)
            {
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
                context.ItemTimeTables.Add(new ItemTimeTable {TimeTableId = timeTable.Id, WorkshopId = workshop.Id});
                context.SaveChanges();
            }
        }
    }
}