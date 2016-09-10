using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace ApiGateway.Core.Filters
{
    public class FilterLoader
    {
        private static readonly List<IFilter> Filters = new List<IFilter>();

        public static void Load()
        {
            var libraries = DependencyContext.Default.CompileLibraries;
            foreach (var library in libraries)
            {
                if (library.Name.StartsWith("ApiGateway"))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    var filterClasses = assembly.DefinedTypes.Where(i => i.IsAssignableFrom(typeof (IFilter)) && i.IsClass && !i.IsAbstract).Select(i => i.AsType());
                    Filters.AddRange(filterClasses.Select(i => Activator.CreateInstance<IFilter>()));
                }
            }
        }

        public static List<IFilter> GetFiltersByType(FilterType filterType)
        {
            return Filters.Where(i => i.FilterType == filterType).ToList();
        }
    }
}
