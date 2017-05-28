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
                .ForMember(i => i.TimeTables, o => o.MapFrom(i => i.ItemTimeTables.Select(r => r.TimeTable)));
            CreateMap<WorkshopModel, Workshop>()
                .ForMember(i => i.ItemTimeTables, o => o.Ignore());
        }
    }
}