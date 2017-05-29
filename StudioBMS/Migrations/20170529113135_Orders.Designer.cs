using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StudioBMS.Database.Context;

namespace StudioBMS.Migrations
{
    [DbContext(typeof(StudioContext))]
    [Migration("20170529113135_Orders")]
    partial class Orders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PersonClaims");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("PersonLogins");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PersonRoles");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonToken", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("PersonTokens");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsPaid");

                    b.Property<long>("OrderNumber");

                    b.Property<Guid>("PerformerId");

                    b.Property<long>("Price");

                    b.Property<Guid>("StatusId");

                    b.Property<Guid>("WorkshopId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PerformerId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.OrderService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ServiceId");

                    b.ToTable("OrderServices");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.PersonService", b =>
                {
                    b.Property<Guid>("ServiceId");

                    b.Property<Guid>("PersonId");

                    b.Property<Guid>("Id");

                    b.HasKey("ServiceId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonServices");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.PersonTimetable", b =>
                {
                    b.Property<Guid>("PersonId");

                    b.Property<Guid>("TimeTableId");

                    b.Property<Guid>("Id");

                    b.HasKey("PersonId", "TimeTableId");

                    b.HasIndex("TimeTableId");

                    b.ToTable("PersonTimetables");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Duration");

                    b.Property<string>("EnName")
                        .IsRequired();

                    b.Property<int>("Price");

                    b.Property<string>("RuName")
                        .IsRequired();

                    b.Property<string>("UkName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.Timetable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("WeekDay");

                    b.HasKey("Id");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.Workshop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Workshops");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.WorkshopTimetable", b =>
                {
                    b.Property<Guid>("TimetableId");

                    b.Property<Guid>("WorkshopId");

                    b.Property<Guid>("Id");

                    b.HasKey("TimetableId", "WorkshopId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("WorkshopTimetables");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonClaim", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Person")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonLogin", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Person")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.PersonRole", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.IdentityBase.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudioBMS.Core.Entities.Person", "Person")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.IdentityBase.RoleClaim", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.IdentityBase.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.Order", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Person", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("StudioBMS.Core.Entities.Person", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId");

                    b.HasOne("StudioBMS.Core.Entities.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudioBMS.Core.Entities.Workshop", "Workshop")
                        .WithMany()
                        .HasForeignKey("WorkshopId");
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.OrderService", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Order", "Order")
                        .WithMany("OrderServices")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudioBMS.Core.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.PersonService", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Person", "Person")
                        .WithMany("PersonServices")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudioBMS.Core.Entities.Service", "Service")
                        .WithMany("PersonServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.PersonTimetable", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Person", "Person")
                        .WithMany("PersonTimetables")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudioBMS.Core.Entities.Timetable", "Timetable")
                        .WithMany("PersonTimetables")
                        .HasForeignKey("TimeTableId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudioBMS.Core.Entities.WorkshopTimetable", b =>
                {
                    b.HasOne("StudioBMS.Core.Entities.Timetable", "Timetable")
                        .WithMany("WorkshopTimetables")
                        .HasForeignKey("TimetableId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudioBMS.Core.Entities.Workshop", "Workshop")
                        .WithMany("WorkshopTimetables")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
