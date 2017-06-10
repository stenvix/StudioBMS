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

        public async Task<Statistic> StatisticsByCustomer(Guid[] customers, DateTime periodStart, DateTime periodEnd)
        {
            var statistic = new Statistic();
            var orders = await _unitOfWork.OrderRepository.FindInPeriod(periodStart, periodEnd);
            foreach (var customerId in customers)
            {
                var customer = await _unitOfWork.PersonRepository.GetAsync(customerId);
                var filterOrders = await _unitOfWork.OrderRepository.FindByCustomer(customerId, orders);
                var barStatistic = new BarStatistic
                {
                    Label = $"{customer.LastName} {customer.FirstName[0]}.",
                    OrderItems = await _unitOfWork.OrderRepository.BarOrdersByPerson(customer, filterOrders),
                    PaymentItems = await _unitOfWork.OrderRepository.BarPaymentByPerson(customer, filterOrders)
                };

                statistic.BarStatistics.Add(barStatistic);
            }

            statistic.PieStatistic =
                await _unitOfWork.OrderRepository.PieStatisticByCustomers(customers, orders);
            statistic.AvarageBills =
                await _unitOfWork.OrderRepository.AvarageBillsByCustomers(customers, orders, periodStart, periodEnd);

            return statistic;
        }

        public async Task<Statistic> StatisticsByWorkers(Guid[] workers, DateTime periodStart, DateTime periodEnd)
        {
            var statistic = new Statistic();
            var orders = await _unitOfWork.OrderRepository.FindInPeriod(periodStart, periodEnd);
            foreach (var workerId in workers)
            {
                var worker = await _unitOfWork.PersonRepository.GetAsync(workerId);
                var filterOrders = await _unitOfWork.OrderRepository.FindByPerformer(workerId, orders);
                var barStatistic = new BarStatistic
                {
                    Label = $"{worker.LastName} {worker.FirstName[0]}.",
                    OrderItems = await _unitOfWork.OrderRepository.BarOrdersByPerson(worker, filterOrders),
                    PaymentItems = await _unitOfWork.OrderRepository.BarPaymentByPerson(worker, filterOrders)
                };

                statistic.BarStatistics.Add(barStatistic);
            }

            statistic.PieStatistic =
                await _unitOfWork.OrderRepository.PieStatisticByWorkers(workers, orders);
            statistic.AvarageBills =
                await _unitOfWork.OrderRepository.AvarageBillsByWorkers(workers, orders, periodStart, periodEnd);

            return statistic;
        }

        public async Task<Statistic> StatisticsByWorkshops(Guid[] workshops, DateTime periodStart, DateTime periodEnd)
        {
            var statistic = new Statistic();
            var orders = await _unitOfWork.OrderRepository.FindInPeriod(periodStart, periodEnd);

            foreach (var workshopId in workshops)
            {
                var workshop = await _unitOfWork.WorkshopRepository.GetAsync(workshopId);
                var filterOrders = await _unitOfWork.OrderRepository.FindByWorkshop(workshopId, orders);
                var barStatistic = new BarStatistic
                {
                    Label = $"{workshop.Title} ({workshop.City})",
                    OrderItems = await _unitOfWork.OrderRepository.BarOrdersByWorkshop(workshop, filterOrders),
                    PaymentItems = await _unitOfWork.OrderRepository.BarPaymentByWorkshop(workshop, filterOrders)
                };

                statistic.BarStatistics.Add(barStatistic);
            }
            
            statistic.PieStatistic =
                await _unitOfWork.OrderRepository.PieStatisticByWorkshop(workshops, orders);
            statistic.AvarageBills =
                await _unitOfWork.OrderRepository.AvarageBillsByWorkshops(workshops, orders, periodStart, periodEnd);
            return statistic;
        }
    }
}