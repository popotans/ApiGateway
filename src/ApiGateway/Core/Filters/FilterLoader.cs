using System;
using System.Collections.Generic;
using System.Linq;
using ApiGateway.Core.IoC;

namespace ApiGateway.Core.Filters
{
    public class FilterLoader
    {
        private static readonly List<IFilter> Filters = new List<IFilter>();

        public static void Load()
        {
            var assemblies = ObjectContainer.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var filterClasses = assembly.DefinedTypes.Where(i => i.IsAssignableFrom(typeof (IFilter)) && i.IsClass && !i.IsAbstract).Select(i => i.AsType());
                Filters.AddRange(filterClasses.Select(i => Activator.CreateInstance<IFilter>()));
            }
        }

        public static List<IFilter> GetFiltersByType(FilterType filterType)
        {
            return Filters.Where(i => i.FilterType == filterType).ToList();
        }
    }
}
