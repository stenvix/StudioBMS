using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.Statistics;

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
            var result = await _unitOfWork.OrderStatusRepository.GetAsync();
            return result.To<OrderStatusModel>();
        }

        public async Task<IList<OrderModel>> FindByCustomer(Guid personId)
        {
            var result = await _unitOfWork.OrderRepository.FindByCustomer(personId);
            return result.To<OrderModel>();
        }

        public async Task<IList<OrderModel>> FindByWorkshop(Guid workshopId)
        {
            var result = await _unitOfWork.OrderRepository.FindByWorkshop(workshopId);
            return result.To<OrderModel>();
        }

        public async Task ManageBalance(PaymentViewModel payment)
        {
            var model = await GetAsync(payment.Id);
            model.Balance += payment.Balance;
            await UpdateAsync(model);
        }

        public async Task Deactivate(Guid id)
        {
            var model = await GetAsync(id);
            var status = _unitOfWork.OrderStatusRepository.Declined.To<OrderStatusModel>();
            model.Status = status;
            await UpdateAsync(model);
        }

        public async Task Done(Guid id)
        {
            var model = await GetAsync(id);
            var status = _unitOfWork.OrderStatusRepository.Done.To<OrderStatusModel>();
            model.Status = status;
            await UpdateAsync(model);
        }

        public async Task<Statistic> StatisticsByCustomer(Guid customerId, DateTime periodStart, DateTime periodEnd)
        {
            var customer = await _unitOfWork.PersonRepository.GetAsync(customerId);
            var statistic = new Statistic();
            var barStatistic = new BarStatistic
            {
                Label = $"{customer.LastName} {customer.FirstName}",
                OrderItems = new List<BarStatisticOrderItem>
                {
                    await _unitOfWork.OrderRepository.BarOrdersByCustomer(customer, periodStart, periodEnd)
                },
                PaymentItems = new List<BarStatisticPaymentItem>
                {
                    await _unitOfWork.OrderRepository.BarPaymentByCustomer(customer, periodStart, periodEnd)
                }
            };
            statistic.BarStatistics = barStatistic;

            return statistic;
        }
    }
}