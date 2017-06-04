using System;
using System.Globalization;
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
        public double Price { get; set; }

        public string Title
        {
            get
            {
                var locale = CultureInfo.CurrentCulture.Name;
                switch (locale)
                {
                    case "en":
                        return EnTitle;
                    case "ru":
                        return RuTitle;
                    default:
                        return UkTitle;
                }
            }
        }
    }
}