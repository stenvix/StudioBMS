using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    internal class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceModel>()
                .ForMember(i => i.Price, o => o.MapFrom(src => src.Price / 100));
            CreateMap<ServiceModel, Service>()
                .ForMember(i => i.Price, o => o.MapFrom(src => src.Price * 100));
        }
    }
}