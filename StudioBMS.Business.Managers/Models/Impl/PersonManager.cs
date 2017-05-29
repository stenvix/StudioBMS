using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            return Mapper.Map<IEnumerable<Person>, IList<PersonModel>>(result);
        }

        public async Task<IList<PersonModel>> GetEmployees(Guid? workshopId)
        {
            var roles = new List<Role>{
                _unitOfWork.RoleRepository.Client,
                _unitOfWork.RoleRepository.Administrator,
                _unitOfWork.RoleRepository.Manager
            };
            var result = await _unitOfWork.PersonRepository.FindByWorkshopAndNotInRoles(workshopId, roles.Select(i => i.Id).ToArray());
            return Mapper.Map<IEnumerable<Person>, IList<PersonModel>>(result);
        }

        public async Task<IList<PersonModel>> GetStaff()
        {
            var roles = new List<Role>
            {
                _unitOfWork.RoleRepository.Client
            };
            var result = await _unitOfWork.PersonRepository.FindByRole(Guid.Empty, roles.Select(i => i.Id).ToArray());
            return Mapper.Map<IEnumerable<Person>, IList<PersonModel>>(result);
        }
    }
}