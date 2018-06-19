using Holdprint.Commom.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Holdprint.EF.Base
{
    public abstract class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions options)
          : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.ApplyAllConfigurationsFromCurrentAssembly(builder);

            base.OnModelCreating(builder);
        }

        private void ApplyAllConfigurationsFromCurrentAssembly(ModelBuilder builder)
        {
            var applyGenericMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration", new Type[] { typeof(IEntityTypeConfiguration<>) });

            IEnumerable<Type> allTypes = ReflectionHelper.GetApplicationTypes(
               null,
               t => !t.IsAbstract && 
                    !t.IsGenericTypeDefinition && 
                    t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                        i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
               );

            foreach (var type in allTypes)
            {
                dynamic config = Activator.CreateInstance(type);
                builder.ApplyConfiguration(config);
            }
        }
    }
}
