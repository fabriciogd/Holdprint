using AutoMapper;
using Holdprint.Commom.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holdprint.Mappers.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            List<Type> allTypes = ReflectionHelper.GetApplicationTypes(
               null,
               t => typeof(Profile).IsAssignableFrom(t)).ToList();

            AutoMapper.Mapper.Initialize(cfg => {
                allTypes.ForEach(cfg.AddProfile);
            });
        }
    }
}
