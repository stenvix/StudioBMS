

using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    public class TimeTableProfile: Profile
    {
        public TimeTableProfile()
        {
            CreateMap<TimeTable, TimeTableModel>();
            CreateMap<TimeTableModel, TimeTable>()
                .ForMember(i => i.ItemTimeTables, o => o.Ignore());
        }
    }
}
