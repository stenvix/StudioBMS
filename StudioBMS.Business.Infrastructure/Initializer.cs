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
using StudioBMS.Business.Identity.Models;
using StudioBMS.Business.Managers.Identity;
using StudioBMS.Business.Managers.Identity.Helpers;
using StudioBMS.Business.Managers.Identity.Stores;
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

            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        public static void DbInitialize(StudioContext context, PersonModelManager manager)
        {
            Task.Run(() =>
            {
                var relationalDatabaseCreator = context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                var existed = relationalDatabaseCreator != null && relationalDatabaseCreator.Exists();
                context.Database.Migrate();

                if (existed)
                    return;

                context.Initialize();

                //var email = "sa@test.com";
                //var password = "Admin123!";
                //var person = new PersonModel { UserName = email, Email = email };
                //var result = await manager.CreateAsync(person, password);

                //if (!result.Succeeded)
                //    throw new ArgumentNullException($"Database fail to initialize person:{nameof(context)}");
                //result = await manager.AddToRoleAsync(person, "manager");

                //if (!result.Succeeded)
                //    throw new ArgumentNullException($"Database fail to initialize person role:{nameof(context)}");
            });
        }
    }
}