using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Holdprint.Commom.Helpers
{
    public static class ReflectionHelper
    {
        private static IEnumerable<Assembly> _appAssemblies;

        public static void SetApplicationAssemblies(IEnumerable<Assembly> assemblies)
        {
            _appAssemblies = assemblies
                .Distinct()
                .Where(a => !a.IsDynamic);
        }

        public static IEnumerable<Type> GetApplicationTypes(Func<Assembly, bool> assemblyFilter = null, Func<Type, bool> typeFilter = null)
        {
            try
            {
                SetApplicationAssemblies(AppDomain.CurrentDomain.GetAssemblies());

                IEnumerable<Assembly> assemblies = _appAssemblies.Where(a =>
                    a.FullName.StartsWith("Holdprint")
                );

                if (assemblyFilter != null)
                    assemblies = assemblies.Where(assemblyFilter);

                IEnumerable<Type> types = assemblies
                    .SelectMany(a => a.GetTypes())
                    .Where(t =>
                        !t.Name.StartsWith("<"));

                if (typeFilter != null)
                    types = types.Where(typeFilter);

                return types.ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                var loaderMessages = e.LoaderExceptions
                    .Select(a => a.Message)
                    .Aggregate((a, b) => a + Environment.NewLine + b);

                throw new TypeLoadException(loaderMessages, e);
            }
        }
    }
}
