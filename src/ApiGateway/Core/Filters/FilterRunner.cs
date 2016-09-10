using System.Linq;

namespace ApiGateway.Core.Filters
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
