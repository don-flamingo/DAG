using System;
using System.Collections.Generic;
using System.IO;
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

        public string ReadTemplate(string templatePath)
            => File.ReadAllText(templatePath);

        protected void CreateClass(string path, string body, bool update)
        {
            path = $"{path}.g.cs";

            if (File.Exists(path) && !update)
                return;

            File.WriteAllText(path, body);
            Console.WriteLine($"{path} created");
        } 

        protected IEnumerable<Type> GetModelsFromAssembly(string modelsNamespace)
        {
            var assembly = Assembly.LoadFrom(_assemblyPath);
            return assembly.GetClassFromAssemblyNamespace(modelsNamespace);
        }

    }
}
