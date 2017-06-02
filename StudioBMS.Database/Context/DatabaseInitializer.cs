using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Database.Context
{
    public static class DatabaseInitializer
    {
        public static async Task Initialize(this StudioContext context)
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
                StringConstants.ActiveStatus,
                StringConstants.DoneStatus,
                StringConstants.DeclinedStatus
            };

            foreach (var status in statuses)
                context.OrderStatuses.Add(new OrderStatus {Name = status});
            await context.SaveChangesAsync();
        }

        private static async Task PersonRoles(StudioContext context)
        {
            var roles = new[]
            {
                StringConstants.CustomerRole,
                StringConstants.ManagerRole,
                StringConstants.AdministratorRole,
                "Hairdresser",
                "MakeupArtist",
                "Stylist",
                "Manicurist",
                "Cosmetologist"
            };

            foreach (var roleName in roles)
            {
                var role = new Role {Name = roleName.Normalize(), NormalizedName = roleName.ToUpper().Normalize()};
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
                new Timetable
                {
                    Start = new DateTime().AddHours(9),
                    End = new DateTime().AddHours(17),
                    WeekDay = DayOfWeek.Monday
                },
                new Timetable
                {
                    Start = new DateTime().AddHours(9),
                    End = new DateTime().AddHours(17),
                    WeekDay = DayOfWeek.Tuesday
                },
                new Timetable
                {
                    Start = new DateTime().AddHours(9),
                    End = new DateTime().AddHours(17),
                    WeekDay = DayOfWeek.Wednesday
                },
                new Timetable
                {
                    Start = new DateTime().AddHours(9),
                    End = new DateTime().AddHours(17),
                    WeekDay = DayOfWeek.Thursday
                },
                new Timetable
                {
                    Start = new DateTime().AddHours(9),
                    End = new DateTime().AddHours(17),
                    WeekDay = DayOfWeek.Friday
                }
            };

            foreach (var timeTable in timeTables)
            {
                context.TimeTables.Add(timeTable);
                context.SaveChanges();
                context.WorkshopTimetables.Add(
                    new WorkshopTimetable {TimetableId = timeTable.Id, WorkshopId = workshop.Id});
                context.SaveChanges();
            }
        }

        private static async Task Services(StudioContext context)
        {
            var services = new List<Service>
            {
                new Service
                {
                    EnTitle = "Haircuts for women",
                    RuTitle = "Стрижка женская",
                    UkTitle = "Стрижка жіноча",
                    Duration = new DateTime().AddHours(1),
                    IsActive = true,
                    Price = 12000
                },
                new Service
                {
                    EnTitle = "Men's grooming",
                    RuTitle = "Стрижка мужская",
                    UkTitle = "Стрижка чоловіча",
                    Duration = new DateTime().AddHours(1),
                    IsActive = true,
                    Price = 6000
                },
                new Service
                {
                    EnTitle = "Children's haircut",
                    RuTitle = "Стрижка детская",
                    UkTitle = "Стрижка дитяча",
                    Duration = new DateTime().AddMinutes(50),
                    IsActive = true,
                    Price = 4500
                },
                new Service
                {
                    EnTitle = "Registration ends of hair",
                    RuTitle = "Оформление концов волос",
                    UkTitle = "Оформлення кінців волосся",
                    Duration = new DateTime().AddMinutes(30),
                    IsActive = true,
                    Price = 5500
                },
                new Service
                {
                    EnTitle = "Making bangs",
                    RuTitle = "Оформление челки",
                    UkTitle = "Оформлення чілки",
                    Duration = new DateTime().AddMinutes(30),
                    IsActive = true,
                    Price = 2500
                },
                new Service
                {
                    EnTitle = "Beard grooming",
                    RuTitle = "Стрижка бороды",
                    UkTitle = "Стрижка бороди",
                    Duration = new DateTime().AddMinutes(30),
                    IsActive = true,
                    Price = 3000
                },
                new Service
                {
                    EnTitle = "Registration mustache",
                    RuTitle = "Оформление усов",
                    UkTitle = "Оформлення вусів",
                    Duration = new DateTime().AddMinutes(30),
                    IsActive = true,
                    Price = 1000
                }
            };

            foreach (var service in services)
            {
                context.Services.Add(service);
                await context.SaveChangesAsync();
            }
        }
    }
}