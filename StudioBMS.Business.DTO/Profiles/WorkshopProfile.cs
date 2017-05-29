using System.Linq;
using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    public class WorkshopProfile : Profile
    {
        public WorkshopProfile()
        {
            CreateMap<Workshop, WorkshopModel>()
                .ForMember(i => i.TimeTables, o => o.MapFrom(i => i.WorkshopTimetables.Select(r => r.Timetable)));
            CreateMap<WorkshopModel, Workshop>()
                .ForMember(i=>i.Persons, o=>o.Ignore())
                .ForMember(i => i.WorkshopTimetables, o => o.Ignore());
        }
    }
}