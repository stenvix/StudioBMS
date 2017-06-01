using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class OrderManager : CrudManager<OrderModel, Order>, IOrderManager
    {
        public OrderManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.OrderRepository)
        {
        }

        public async Task<OrderModel> CreateAsync(OrderViewModel item)
        {
            var order = Mapper.Map<OrderViewModel, OrderModel>(item);
            order.Status = _unitOfWork.OrderStatusRepository.Active.To<OrderStatusModel>();
            foreach (var service in order.Services)
            {
                
                var srv = await _unitOfWork.ServiceRepository.GetAsync(service.Id);
                order.Price += srv.Price / 100.0;
            }
            order.OrderNumber = _unitOfWork.OrderRepository.Count() + 1;
            order = await CreateAsync(order);
            foreach (var serviceId in item.ServiceIds)
            {
                var entity = new OrderService
                {
                    ServiceId = serviceId,
                    OrderId = order.Id
                };
                await _unitOfWork.OrderServiceRepository.CreateAsync(entity);
            }
            await _unitOfWork.SaveChanges();
            return order;
        }

        public async Task<IList<OrderStatusModel>> GetStatuses()
        {
            var result =await _unitOfWork.OrderStatusRepository.GetAsync();
            return result.To<OrderStatusModel>();
        }
    }
}