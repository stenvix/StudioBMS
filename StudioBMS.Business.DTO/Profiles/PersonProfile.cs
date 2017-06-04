using System;
using System.Linq;
using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonModel>()
                .ForMember(i=>i.Roles, o=>o.MapFrom(src=>src.Roles))
                .ForMember(i=>i.Role, o=>o.MapFrom(src=> src.Roles.Select(r=>r.Role).FirstOrDefault()))
                .ForMember(i=>i.TimeTables, o=>o.MapFrom(src=>src.PersonTimetables.Select(i=>i.Timetable)))
                .ForMember(i=>i.Orders, o=>o.Ignore());
            CreateMap<PersonModel, Person>()
                .ForMember(i=>i.WorkshopId, o=>o.MapFrom(src=>src.Workshop.Id))
                .ForMember(i=>i.Workshop, o=>o.Ignore())
                .ForMember(i=>i.Claims, o=>o.Ignore())
                .ForMember(i=>i.PersonTimetables, o=>o.Ignore())
                .ForMember(i=>i.Logins, o=>o.Ignore())
                .ForMember(i=>i.Roles, o=>o.Ignore())
                .ForMember(i=>i.IsActive, o=>o.MapFrom(src=> true));
        }
    }
}