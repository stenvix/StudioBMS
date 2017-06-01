using System;

namespace StudioBMS.Business.DTO.Models
{
    public class OrderViewModel
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
    }
}
