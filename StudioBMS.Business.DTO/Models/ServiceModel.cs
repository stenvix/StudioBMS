using System;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class ServiceModel : IModel
    {
        public Guid Id { get; set; }
        public string EnTitle { get; set; }
        public string RuTitle { get; set; }
        public string UkTitle { get; set; }
        public DateTime Duration { get; set; }
        public int Price { get; set; }
    }
}