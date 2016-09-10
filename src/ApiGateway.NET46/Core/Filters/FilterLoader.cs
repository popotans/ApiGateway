using System;
using System.Collections.Generic;
using System.Linq;
using ApiGateway.NET46.Core.IoC;

namespace ApiGateway.NET46.Core.Filters
{
    public class FilterLoader
    {
        private static readonly List<IFilter> Filters = new List<IFilter>();

        public static void Load()
        {
            var assemblies = ObjectContainer.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var filterClasses = assembly.DefinedTypes.Where(i => typeof(IFilter).IsAssignableFrom(i) && i.IsClass && !i.IsAbstract).Select(i => i.AsType());
                Filters.AddRange(filterClasses.Select(i => (IFilter) Activator.CreateInstance(i)));
            }
        }

        public static List<IFilter> GetFiltersByType(FilterType filterType)
        {
            return Filters.Where(i => i.FilterType == filterType).ToList();
        }
    }
}
