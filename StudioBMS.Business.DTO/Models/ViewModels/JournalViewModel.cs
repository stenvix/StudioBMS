using System;

namespace StudioBMS.Business.DTO.Models.ViewModels
{
    public class JournalViewModel
    {
        public Guid[] WorkerIds { get; set; }
        public DateTime Date { get; set; }
    }
}