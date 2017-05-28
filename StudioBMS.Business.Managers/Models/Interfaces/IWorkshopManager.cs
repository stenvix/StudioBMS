using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Interfaces;

namespace StudioBMS.Business.Managers.Models.Interfaces
{
    public interface IWorkshopManager : IManager<WorkshopModel>
    {
        Task<IList<TimeTableModel>> GetTimeTables(Guid workshopId, CancellationToken cancellationToken = default(CancellationToken));
    }
}