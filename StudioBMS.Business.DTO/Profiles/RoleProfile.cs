using AutoMapper;
using StudioBMS.Business.DTO.Models;
using StudioBMS.Business.Identity.Models;
using StudioBMS.Core.Entities.IdentityBase;

namespace StudioBMS.Business.DTO.Profiles
{
    internal class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
            CreateMap<PersonRole, PersonModelRole>();
        }
    }
}