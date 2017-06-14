using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Database.Context
{
    public static class DatabaseInitializer
    {
        public static string[] MakeupServices = { "Makeup", "Wedding makeup", "Day makeup" };
        public static string[] StylistServices = {"Consultation on the image", "Wardrobe analysis"};
        public static string[] ManicuristServices = {"Manicure", "French manicure", "Removal of gel-varnish"};
        public static string[] CosmetologistServices = {"Face cleaning", "Peeling"};
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
                context.OrderStatuses.Add(new OrderStatus { Name = status });
            await context.SaveChangesAsync();
        }

        private static async Task PersonRoles(StudioContext context)
        {
            var roles = new[]
            {
                StringConstants.AdministratorRole,
                StringConstants.CustomerRole,
                StringConstants.ManagerRole,
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
            var workshopTitles = new[] { "FirstPoint", "SecondPoint" };
            var workshopAddress = new[] { "Лукаша 5", "Б. Хмельницького 18" };
            
            for (int i = 0; i < 2; i++)
            {
                var workshop = new Workshop
                {
                    Title = workshopTitles[i],
                    City = "Львів",
                    Address = workshopAddress[i],
                    Email = $"{workshopTitles[i].ToLower()}@studio.com",
                    PhoneNumber = "0999999999"
                };
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
                        new WorkshopTimetable { TimetableId = timeTable.Id, WorkshopId = workshop.Id });
                    context.SaveChanges();
                }
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
                },
                new Service
                {
                    EnTitle = MakeupServices[0],
                    RuTitle = "Макияж",
                    UkTitle = "Макіяж",
                    Duration = new DateTime().AddHours(1).AddMinutes(30),
                    IsActive = true,
                    Price = 50000
                },
                new Service
                {
                    EnTitle = MakeupServices[1],
                    RuTitle = "Свадебный макияж",
                    UkTitle = "Весільний макіяж",
                    Duration = new DateTime().AddHours(2).AddMinutes(30),
                    IsActive = true,
                    Price = 80000
                },
                new Service
                {
                    EnTitle = MakeupServices[2],
                    RuTitle = "Дневной макияж",
                    UkTitle = "Повсякденний макіяж",
                    Duration = new DateTime().AddHours(1).AddMinutes(0),
                    IsActive = true,
                    Price = 35000
                },
                new Service
                {
                    EnTitle = StylistServices[0],
                    RuTitle = "Консультация по имиджу",
                    UkTitle = "Консультація по іміджу",
                    Duration = new DateTime().AddHours(2).AddMinutes(0),
                    IsActive = true,
                    Price = 25000
                },
                new Service
                {
                    EnTitle = StylistServices[1],
                    RuTitle = "Анализ гардероба",
                    UkTitle = "Аналіз гардеробу",
                    Duration = new DateTime().AddHours(3).AddMinutes(0),
                    IsActive = true,
                    Price = 45000
                },
                new Service
                {
                    EnTitle = ManicuristServices[0],
                    RuTitle = "Маникюр",
                    UkTitle = "Манікюр",
                    Duration = new DateTime().AddHours(1).AddMinutes(0),
                    IsActive = true,
                    Price = 15000
                },
                new Service
                {
                    EnTitle = ManicuristServices[1],
                    RuTitle = "Французский маникюр",
                    UkTitle = "Французький манікюр",
                    Duration = new DateTime().AddHours(1).AddMinutes(0),
                    IsActive = true,
                    Price = 20000
                },
                new Service
                {
                    EnTitle = ManicuristServices[2],
                    RuTitle = "Снятие гель-лака",
                    UkTitle = "Зняття гель-лаку",
                    Duration = new DateTime().AddMinutes(45),
                    IsActive = true,
                    Price = 10000
                },
                new Service
                {
                    EnTitle = CosmetologistServices[0],
                    RuTitle = "Чистка лица",
                    UkTitle = "Чистка обличчя",
                    Duration = new DateTime().AddMinutes(50),
                    IsActive = true,
                    Price = 12000
                },
                new Service
                {
                    EnTitle = CosmetologistServices[1],
                    RuTitle = "Пилинг",
                    UkTitle = "Пілінг",
                    Duration = new DateTime().AddMinutes(45),
                    IsActive = true,
                    Price = 7000
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