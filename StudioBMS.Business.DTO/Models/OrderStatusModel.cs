using System;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class OrderStatusModel : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}