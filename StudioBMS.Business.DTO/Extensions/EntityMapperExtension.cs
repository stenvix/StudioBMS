using System.Collections.Generic;
using AutoMapper;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Business.DTO.Extensions
{
    public static class EntityMapperExtension
    {
        public static TTo To<TTo>(this IEntity entity)
        {
            return Mapper.Map<TTo>(entity);
        }

        public static IList<TTo> To<TTo>(this IEnumerable<IEntity> entities) where TTo : class, IModel
        {
            return Mapper.Map<IList<TTo>>(entities);
        }
    }
}