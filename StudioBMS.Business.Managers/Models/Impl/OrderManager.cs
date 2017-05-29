﻿using StudioBMS.Business.DTO.Models;
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
    }
}