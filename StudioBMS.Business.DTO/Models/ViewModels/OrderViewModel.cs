using System;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    //Entity because extencion mapping
    public class OrderViewModel: IEntity 
    {
        public Guid CustomerId { get; set; }
        public Guid WorkshopId { get; set; }
        public Guid PerformerId { get; set; }
        public Guid[] ServiceIds { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }

        //Additional fieds
        public Guid StatusId { get; set; }
        public bool IsPaid { get; set; }
        public int OrderNumber { get; set; }
        public double Price { get; set; }
        public double Balance { get; set; }
        public Guid Id { get; set; }
    }
}
