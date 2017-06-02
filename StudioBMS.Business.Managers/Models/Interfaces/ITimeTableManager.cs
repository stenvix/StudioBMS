using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface ITimeTableManager : IManager<TimeTableModel>
    {
        Task CreateWorkshop(Guid workshopId, Guid timetableId);
        Task CreateWorker(Guid workshopId, Guid timetableId);
        Task<IList<TimeTableModel>> FindByWorker(Guid workerId);
    }
}