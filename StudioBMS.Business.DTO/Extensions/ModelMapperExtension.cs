﻿using System.Collections.Generic;
using AutoMapper;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Business.DTO.Extensions
{
    public static class ModelMapperExtension
    {
        public static TTo To<TTo>(this IModel model) where TTo : class, IEntity
        {
            return Mapper.Map<TTo>(model);
        }
    }
}