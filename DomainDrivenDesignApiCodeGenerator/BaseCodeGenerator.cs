using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Helpers;

namespace DomainDrivenDesignApiCodeGenerator
{
    public abstract class BaseCodeGenerator
    {
        protected readonly string _assemblyPath;

        public BaseCodeGenerator(string assemblyPath)
        {
            _assemblyPath = assemblyPath;
        }


        public abstract void Generate();

        protected IEnumerable<Type> GetModelsFromAssembly(string modelsNamespace)
        {
            var assembly = Assembly.LoadFrom(_assemblyPath);
            return assembly.GetClassFromAssemblyNamespace(modelsNamespace);
        }

    }
}
