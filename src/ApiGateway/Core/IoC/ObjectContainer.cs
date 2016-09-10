﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace ApiGateway.Core.IoC
{
    public class ObjectContainer
    {
        private static IContainer _container;
        private static Assembly[] _assemblies;

        public static void Init(IServiceCollection services)
        {
            LoadAssemblies();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyModules(_assemblies);
            containerBuilder.RegisterAssemblyTypes(_assemblies);
            containerBuilder.Populate(services);

            _container = containerBuilder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static Assembly[] GetAssemblies()
        {
            if (_assemblies == null) LoadAssemblies();
            return _assemblies;
        }

        private static void LoadAssemblies()
        {
            var libraries = DependencyContext.Default.CompileLibraries;
            _assemblies = libraries.Where(i => i.Name.StartsWith("ApiGateway")).Select(library => Assembly.Load(new AssemblyName(library.Name))).ToArray();
        }
    }
}
