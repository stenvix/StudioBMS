using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.DTO.Profiles;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Identity.Stores;
using StudioBMS.Business.Managers.Models.Impl;
using StudioBMS.Business.Managers.Repositories.Impl;
using StudioBMS.Core.Entities;
using StudioBMS.Database.Context;

namespace StudioBMS.Business.Infrastructure
{
    public class Initializer
    {
        public static string[] Names =
        {
            "Августа", "Агата","Агнесса","Лейла", "Сандра","Софья", "Таисия", "Моника", "Надежда", "Роза"
        };

        public static string[] LastNames =
        {
            "Баранова", "Беляева", "Денисова","Зайцев", "Карпова", "Котова", "Петрова", "Самойлова", "Тихонова", "Фадеева"
        };
        public static IServiceProvider InitServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PersonProfile));

            services.AddIdentity<PersonModel, RoleModel>()
                .AddSignInManager<PersonModelSignInManager>()
                .AddUserManager<PersonModelManager>()
                .AddUserStore<PersonModelStore>()
                .AddRoleStore<RoleModelStore>()
                .AddDefaultTokenProviders();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();
            builder.RegisterType<WorkshopManager>().AsImplementedInterfaces();
            builder.RegisterType<TimeTableManager>().AsImplementedInterfaces();
            builder.RegisterType<ServiceManager>().AsImplementedInterfaces();
            builder.RegisterType<PersonManager>().AsImplementedInterfaces();
            builder.RegisterType<OrderManager>().AsImplementedInterfaces();
            builder.RegisterType<RoleManager>().AsImplementedInterfaces();
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        public static void DbInitialize(StudioContext context, PersonModelManager manager)
        {
            Task.Run(async () =>
            {
                var relationalDatabaseCreator = context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                var existed = relationalDatabaseCreator != null && relationalDatabaseCreator.Exists();
                context.Database.Migrate();

                if (existed)
                    return;

                await context.Initialize();

                var workshop = context.Workshops.First().To<WorkshopModel>();
                var workshop2 = context.Workshops.Skip(1).First().To<WorkshopModel>();

                //Init admin
                var roleName = "administrator";
                var email = "sa@test.com";
                var password = "Admin123!";
                var person = new PersonModel
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Valentyn",
                    LastName = "Stepanov",
                    PhoneNumber = "0998877332",
                    Birthday = new DateTime(1994, 9, 18),
                    Workshop = workshop,
                    Language = "uk"
                };
                var result = await manager.CreateAsync(person, password);

                if (!result.Succeeded)
                    throw new ArgumentNullException($"Database fail to initialize person:{nameof(context)}");

                person = await manager.FindByEmailAsync(email);
                result = await manager.AddToRoleAsync(person, roleName);

                if (!result.Succeeded)
                    throw new ArgumentNullException($"Database fail to initialize person role:{nameof(context)}");

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
                    context.PersonTimetables.Add(new PersonTimetable { TimetableId = timeTable.Id, PersonId = person.Id });
                    context.SaveChanges();
                }
                var random = new Random();
                var roleIndex = 0;
                //Init employees
                foreach (var role in context.Roles.Skip(2).ToList())
                {
                    email = $"{role.Name.ToLower()}@test.com";
                    password = "Worker123!";
                    person = new PersonModel
                    {
                        FirstName = Names[random.Next(0, Names.Length)],
                        LastName = LastNames[random.Next(0, Names.Length)],
                        Birthday = new DateTime(random.Next(1965, 1985), random.Next(1, 12), random.Next(1,31)),
                        UserName = email,
                        Email = email,
                        PhoneNumber = $"099112{random.Next(0,9)}334",
                        Workshop = roleIndex%2==1?workshop: workshop2,
                        Language = "uk"
                    };

                    await manager.CreateAsync(person, password);
                    person = await manager.FindByEmailAsync(email);
                    await manager.AddToRoleAsync(person, role.Name);

                    timeTables = new List<Timetable>
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
                        await context.SaveChangesAsync();
                        context.PersonTimetables.Add(new PersonTimetable { TimetableId = timeTable.Id, PersonId = person.Id });
                    }
                    await context.SaveChangesAsync();

                    ///SERVICES
                    if (roleIndex == 1)
                    {
                        foreach (var service in context.Services.Take(7))
                        {
                            context.PersonServices.Add(new PersonService { PersonId = person.Id, ServiceId = service.Id });
                        }
                    }
                    if (roleIndex == 2)
                    {
                        foreach (var service in context.Services.Skip(7).Take(3))
                        {
                            context.PersonServices.Add(new PersonService { PersonId = person.Id, ServiceId = service.Id });
                        }
                    }
                    if (roleIndex == 3)
                    {
                        foreach (var service in context.Services.Skip(10).Take(2))
                        {
                            context.PersonServices.Add(new PersonService { PersonId = person.Id, ServiceId = service.Id });
                        }
                    }
                    if (roleIndex == 4)
                    {
                        foreach (var service in context.Services.Skip(12).Take(3))
                        {
                            context.PersonServices.Add(new PersonService { PersonId = person.Id, ServiceId = service.Id });
                        }
                    }
                    if (roleIndex == 5)
                    {
                        foreach (var service in context.Services.Skip(15).Take(2))
                        {
                            context.PersonServices.Add(new PersonService { PersonId = person.Id, ServiceId = service.Id });
                        }
                    }

                    context.SaveChanges();
                    roleIndex++;
                }

                //Init clients
                //for (int i = 0; i < 5; i++)
                //{
                //    roleName = "client";
                //    email = $"client{i}@test.com";
                //    password = "Client123!";
                //    person = new PersonModel
                //    {
                //        FirstName = $"Client{i}",
                //        LastName = $"LastClient{i}",
                //        Birthday = new DateTime(1987, 01, 21),
                //        UserName = email,
                //        Email = email,
                //        PhoneNumber = "0999944888",
                //        Workshop = workshop
                //    };

                //    result = await manager.CreateAsync(person, password);
                //    if (!result.Succeeded)
                //        throw new ArgumentException($"Database fail to initialize person: {nameof(context)}");

                //    person = await manager.FindByEmailAsync(email);
                //    result = await manager.AddToRoleAsync(person, roleName);

                //    if (!result.Succeeded)
                //        throw new ArgumentNullException($"Database fail to initialize person role:{nameof(context)}");
                //}
            });
        }
    }
}