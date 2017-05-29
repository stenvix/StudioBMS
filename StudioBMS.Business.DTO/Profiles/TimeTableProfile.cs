using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    public class TimeTableProfile : Profile
    {
        public TimeTableProfile()
        {
            CreateMap<Timetable, TimeTableModel>();
            CreateMap<TimeTableModel, Timetable>()
                .ForMember(i => i.PersonTimetables, o => o.Ignore())
                .ForMember(i => i.WorkshopTimetables, o => o.Ignore());
        }
    }
}