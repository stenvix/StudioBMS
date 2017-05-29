using System.Linq;
using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(i=>i.Services, o=>o.MapFrom(src=>src.OrderServices.Select(i=>i.Service)));
            CreateMap<OrderModel, Order>()
                .ForMember(i=>i.CustomerId, o=>o.MapFrom(src=>src.Customer.Id))
                .ForMember(i=>i.WorkshopId, o=>o.MapFrom(src=>src.Workshop.Id))
                .ForMember(i=>i.PerformerId, o=>o.MapFrom(src=>src.Performer.Id))
                .ForMember(i=>i.StatusId, o=>o.MapFrom(src=>src.Status.Id))
                .ForMember(i=>i.Customer, o=>o.Ignore())
                .ForMember(i=>i.Workshop, o=>o.Ignore())
                .ForMember(i=>i.Performer, o=>o.Ignore())
                .ForMember(i=>i.Status, o=>o.Ignore())
                .ForMember(i=>i.OrderServices, o=>o.Ignore());
        }
    }
}