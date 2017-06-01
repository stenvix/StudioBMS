using System.Collections.Generic;
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
                .ForMember(i => i.Services, o => o.MapFrom(src => src.OrderServices.Select(i => i.Service)))
                .ForMember(i => i.Price, o => o.MapFrom(src => src.Price / 100.0))
                .ForMember(i => i.Balance, o => o.MapFrom(src => src.Balance / 100.0))
                .PreserveReferences();
            CreateMap<OrderModel, Order>()
                .ForMember(i => i.CustomerId, o => o.MapFrom(src => src.Customer.Id))
                .ForMember(i => i.WorkshopId, o => o.MapFrom(src => src.Workshop.Id))
                .ForMember(i => i.PerformerId, o => o.MapFrom(src => src.Performer.Id))
                .ForMember(i => i.StatusId, o => o.MapFrom(src => src.Status.Id))
                .ForMember(i => i.Customer, o => o.Ignore())
                .ForMember(i => i.Workshop, o => o.Ignore())
                .ForMember(i => i.Performer, o => o.Ignore())
                .ForMember(i => i.Status, o => o.Ignore())
                .ForMember(i => i.OrderServices, o => o.Ignore())
                .ForMember(i => i.Price, o => o.MapFrom(src => src.Price * 100))
                .ForMember(i => i.Balance, o => o.MapFrom(src => src.Balance * 100))
                .ForMember(i => i.IsPaid, o => o.MapFrom(src => src.Balance >= src.Price));
            CreateMap<OrderViewModel, OrderModel>()
                .ForMember(i => i.Customer, o => o.MapFrom(src => new PersonModel { Id = src.CustomerId }))
                .ForMember(i => i.Workshop, o => o.MapFrom(src => new WorkshopModel { Id = src.WorkshopId }))
                .ForMember(i => i.Performer, o => o.MapFrom(src => new PersonModel { Id = src.PerformerId }))
                .ForMember(i => i.Status, o => o.MapFrom(src => new OrderStatusModel { Id = src.StatusId }))
                .ForMember(i => i.Services, o => o.ResolveUsing(src =>
                   {
                       var list = new List<ServiceModel>();
                       foreach (var serviceId in src.ServiceIds)
                       {
                           list.Add(new ServiceModel { Id = serviceId });
                       }
                       return list;
                   }));

            CreateMap<OrderModel, OrderViewModel>()
                .ForMember(i => i.CustomerId, o => o.MapFrom(src => src.Customer.Id))
                .ForMember(i => i.WorkshopId, o => o.MapFrom(src => src.Workshop.Id))
                .ForMember(i => i.PerformerId, o => o.MapFrom(src => src.Performer.Id))
                .ForMember(i => i.ServiceIds, o => o.MapFrom(src => src.Services.Select(i => i.Id)))
                .ForMember(i => i.StatusId, o => o.MapFrom(src => src.Status.Id))
                .ForMember(i => i.IsPaid, o => o.MapFrom(src => src.IsPaid));
        }
    }
}