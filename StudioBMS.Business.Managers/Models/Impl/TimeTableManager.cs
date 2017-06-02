using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class TimeTableManager : CrudManager<TimeTableModel, Timetable>, ITimeTableManager
    {
        public TimeTableManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.TimeTableRepository)
        {
        }

        public async Task CreateWorkshop(Guid workshopId, Guid timetableId)
        {
            await _unitOfWork.TimeTableRepository.CreateWorkshop(workshopId, timetableId);
            await _unitOfWork.SaveChanges();
        }

        public async Task CreateWorker(Guid workerId, Guid timetableId)
        {
            await _unitOfWork.TimeTableRepository.CreateWorker(workerId, timetableId);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IList<TimeTableModel>> FindByWorker(Guid workerId)
        {
            var result = await _unitOfWork.TimeTableRepository.FindByWorker(workerId);
            return result.To<TimeTableModel>();
        }
    }
}