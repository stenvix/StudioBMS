using AutoMapper;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Business.DTO.Extensions
{
    public static class EntityMapperExtension
    {
        public static TTo To<TTo>(this IEntity entity)
        {
            return Mapper.Map<TTo>(entity);
        }
    }
}