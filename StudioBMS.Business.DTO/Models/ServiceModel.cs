using System;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class ServiceModel : IModel
    {
        public string EnName { get; set; }
        public string RuName { get; set; }
        public string UkName { get; set; }
        public DateTime Duration { get; set; }
        public int Price { get; set; }
        public Guid Id { get; set; }
    }
}