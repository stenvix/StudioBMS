using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class WorkshopManager : CrudManager<WorkshopModel, Workshop>, IWorkshopManager
    {
        public WorkshopManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.WorkshopRepository)
        {
        }

        public async Task<IList<TimeTableModel>> GetTimeTables(Guid workshopId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Mapper.Map<IQueryable<TimeTable>,IList<TimeTableModel>>(await _unitOfWork.TimeTableRepository.ByWorkshop(workshopId, cancellationToken));
        }
    }
}