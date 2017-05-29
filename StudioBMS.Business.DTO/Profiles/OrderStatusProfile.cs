using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.DTO.Profiles
{
    internal class OrderStatusProfile : Profile
    {
        public OrderStatusProfile()
        {
            CreateMap<OrderStatus, OrderStatusModel>();
            CreateMap<OrderStatusModel, OrderStatus>();
        }
    }
}