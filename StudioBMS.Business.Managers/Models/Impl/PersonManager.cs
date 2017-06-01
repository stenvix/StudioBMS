using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Managers.Models.Base.Impl;
using StudioBMS.Business.Managers.Models.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Business.Managers.Models.Impl
{
    public class PersonManager : CrudManager<PersonModel, Person>, IPersonManager
    {
        public PersonManager(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.PersonRepository)
        {
        }

        public async Task<IList<PersonModel>> GetClients()
        {
            var clientRole = _unitOfWork.RoleRepository.Client;
            var result = await _unitOfWork.PersonRepository.FindByRole(clientRole.Id);
            return result.To<PersonModel>();
        }

        public async Task<IList<PersonModel>> GetEmployees(Guid workshopId = default(Guid))
        {
            var roles = new List<Role>{
                _unitOfWork.RoleRepository.Client,
                _unitOfWork.RoleRepository.Administrator,
                _unitOfWork.RoleRepository.Manager
            };
            var result = await _unitOfWork.PersonRepository.FindByWorkshopAndNotInRoles(roles.Select(i => i.Id).ToArray(), workshopId);
            return result.To<PersonModel>();
        }

        public async Task<IList<PersonModel>> GetStaff()
        {
            var roles = new List<Role>
            {
                _unitOfWork.RoleRepository.Client
            };
            var result = await _unitOfWork.PersonRepository.FindByRole(Guid.Empty, roles.Select(i => i.Id).ToArray());
            return result.To<PersonModel>();
        }

        public async Task<IList<PersonModel>> GetWithPerformerOrders(Guid[] ids, DateTime date = default(DateTime))
        {
            List<PersonModel> performers = new List<PersonModel>();
            foreach (var performerId in ids)
            {
                var performer = await _unitOfWork.PersonRepository.GetAsync(performerId);
                if(performer==null)
                    continue;
                var orders = await _unitOfWork.OrderRepository.FindByPerformer(performerId, date);
                var performerModel = performer.To<PersonModel>();
                performerModel.Orders = orders.To<OrderModel>();
                performers.Add(performerModel);
            }
            return performers;
        }

        public async Task<PersonModel> FindByName(string username)
        {
            var result = await _unitOfWork.PersonRepository.FindByName(username);
            return result.To<PersonModel>();
        }
    }
}