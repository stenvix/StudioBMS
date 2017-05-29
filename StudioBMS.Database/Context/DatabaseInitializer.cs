using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Database.Context
{
    public static class DatabaseInitializer
    {
        public static async void Initialize(this StudioContext context)
        {
            await OrderStatuses(context);
            await PersonRoles(context);
            await Workshops(context);
            await Services(context);
        }


        private static async Task OrderStatuses(StudioContext context)
        {
            var statuses = new[]
            {
                "Active",
                "Declined",
                "Done"
            };

            foreach (var status in statuses)
            {
                context.OrderStatuses.Add(new OrderStatus { Name = status });
            }
            await context.SaveChangesAsync();
        }

        private static async Task PersonRoles(StudioContext context)
        {
            var roles = new[]
            {
                "Client",
                "Manager",
                "Employee",
                "Administrator",
                "Hairdresser",
                "MakeupArtist",
                "Stylist",
                "Manicurist",
                "Cosmetologist"
            };

            foreach (var roleName in roles)
            {
                var role = new Role { Name = roleName.Normalize(), NormalizedName = roleName.ToUpper().Normalize() };
                context.Roles.Add(role);
            }
            await context.SaveChangesAsync();
        }

        private static async Task Workshops(StudioContext context)
        {
            var workshop = new Workshop();
            workshop.Title = "FirstPoint";
            workshop.City = "Львів";
            workshop.Address = "Лукаша 5";
            workshop.Email = "first@studio.com";
            workshop.PhoneNumber = "0999999999";
            context.Workshops.Add(workshop);
            await context.SaveChangesAsync();

            var timeTables = new List<Timetable>
            {
                new Timetable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Monday},
                new Timetable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Tuesday},
                new Timetable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Wednesday},
                new Timetable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Thursday},
                new Timetable{Start = new DateTime().AddHours(9), End = new DateTime().AddHours(17), WeekDay = DayOfWeek.Friday}
            };

            foreach (var timeTable in timeTables)
            {
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
                context.WorkshopTimetables.Add(new WorkshopTimetable { TimetableId = timeTable.Id, WorkshopId = workshop.Id });
                context.SaveChanges();
            }
        }

        private static async Task Services(StudioContext context)
        {
            var services = new List<Service>
            {
                new Service{EnName = "Haircuts for women", RuName = "Стрижка женская", UkName = "Стрижка жіноча", Duration = new DateTime().AddHours(1), Price = 12000},
                new Service{EnName = "", RuName = "Стрижка мужская", UkName = "Стрижка чоловіча", Duration = new DateTime().AddHours(1), Price = 6000},
                new Service{EnName = "", RuName = "Стрижка детская", UkName = "Стрижка дитяча", Duration = new DateTime().AddMinutes(50), Price = 4500},
                new Service{EnName = "", RuName = "Оформление концов волос", UkName = "Оформлення кінців волосся", Duration = new DateTime().AddMinutes(30), Price = 5500},
                new Service{EnName = "", RuName = "Оформление челки", UkName = "Оформлення чілки", Duration = new DateTime().AddMinutes(30), Price = 2500},
                new Service{EnName = "", RuName = "Стрижка бороды", UkName = "Стрижка бороди", Duration = new DateTime().AddMinutes(30), Price = 3000},
                new Service{EnName = "", RuName = "Оформление усов", UkName = "Оформлення вусів", Duration = new DateTime().AddMinutes(30), Price = 1000}
            };

            foreach (var service in services)
            {
                context.Services.Add(service);
                await context.SaveChangesAsync();
            }
        }
    }
}