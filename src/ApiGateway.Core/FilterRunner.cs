using System.Linq;
using ApiGateway.Core.Filters;

namespace ApiGateway.Core
{
    public class FilterRunner
    {
        public static void PreRoute()
        {
            Run(FilterType.Pre);
        }

        public static void PostRoute()
        {
            Run(FilterType.Post);
        }

        public static void Route()
        {
            Run(FilterType.Route);
        }

        public static void Error()
        {
            Run(FilterType.Error);
        }

        private static void Run(FilterType filterType)
        {
            var filters = FilterLoader.GetFiltersByType(filterType).OrderBy(i => i.FilterOrder);
            foreach (var filter in filters)
            {
                filter.Execute();
            }
        }
    }
}
