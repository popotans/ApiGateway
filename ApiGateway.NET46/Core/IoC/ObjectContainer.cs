using System.Linq;
using System.Reflection;
using Autofac;

namespace ApiGateway.NET46.Core.IoC
{
    public class ObjectContainer
    {
        private static readonly IContainer Container;
        private static Assembly[] _assemblies;

        static ObjectContainer()
        {
            LoadAssemblies();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyModules(_assemblies);
            containerBuilder.RegisterAssemblyTypes(_assemblies).AsImplementedInterfaces();

            Container = containerBuilder.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static Assembly[] GetAssemblies()
        {
            return _assemblies;
        }

        private static void LoadAssemblies()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyNames = assembly.GetReferencedAssemblies();
            _assemblies = assemblyNames.Select(Assembly.Load).Concat(new[] {assembly}).ToArray();
        }
    }
}
