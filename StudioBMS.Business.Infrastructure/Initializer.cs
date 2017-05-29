using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.DTO.Profiles;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Identity.Stores;
using StudioBMS.Business.Managers.Models.Impl;
using StudioBMS.Business.Managers.Repositories.Impl;
using StudioBMS.Database.Context;

namespace StudioBMS.Business.Infrastructure
{
    public class Initializer
    {
        public static IServiceProvider InitServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PersonProfile));

            services.AddIdentity<PersonModel, RoleModel>()
                .AddSignInManager<PersonModelSignInManager>()
                .AddUserManager<PersonModelManager>()
                .AddUserStore<PersonModelStore>()
                .AddRoleStore<RoleModelStore>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();
            builder.RegisterType<WorkshopManager>().AsImplementedInterfaces();
            builder.RegisterType<TimeTableManager>().AsImplementedInterfaces();
            builder.RegisterType<ServiceManager>().AsImplementedInterfaces();
            builder.RegisterType<PersonManager>().AsImplementedInterfaces();

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

                context.Initialize();

                var roleName = "administrator";
                var email = "sa@test.com";
                var password = "Admin123!";
                var person = new PersonModel
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Valentyn",
                    LastName = "Stepanov",
                    Birthday = new DateTime(1994, 9, 18)
                };
                var result = await manager.CreateAsync(person, password);


                if (!result.Succeeded)
                    throw new ArgumentNullException($"Database fail to initialize person:{nameof(context)}");

                person = await manager.FindByEmailAsync(email);
                result = await manager.AddToRoleAsync(person, roleName);

                if (!result.Succeeded)
                    throw new ArgumentNullException($"Database fail to initialize person role:{nameof(context)}");
            });
        }
    }
}