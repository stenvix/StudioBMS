﻿using System;
using System.Collections.Generic;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class OrderModel : IModel
    {
        public Guid Id { get; set; }
        public OrderStatusModel Status { get; set; }
        public int OrderNumber { get; set; }
        public PersonModel Customer { get; set; }
        public WorkshopModel Workshop { get; set; }
        public PersonModel Performer { get; set; }
        public IList<ServiceModel> Services { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public string PaidStatus => IsPaid ? "Paid" : "NotPaid";

    }
}