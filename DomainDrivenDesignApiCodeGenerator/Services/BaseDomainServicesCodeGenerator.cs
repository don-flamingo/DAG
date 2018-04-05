using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Helpers;
using DomainDrivenDesignApiCodeGenerator.Repositories;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public abstract class BaseDomainServicesCodeGenerator : BaseClassesFromModelsCodeGenerator
    {
        public const string AllTypes = "$All";

        private readonly string _assemblyDtoPath;
        private readonly string _dtoNamespace;
        private readonly IList<string> _ignoredNamespaces;

        private readonly IDictionary<string, IList<string>> _ignoredProps;

        public BaseDomainServicesCodeGenerator(string assemblyDtoPath, string assemblyPath, string usingNamespaces,
            string modelsNamepace, string generateClassesNamespace, string classDirectoryPath, string dtoNamespace,
            IList<string> ignoredNamespaces, bool update, string templatePath, string classNameTemplate )
            : base(modelsNamepace, generateClassesNamespace, classDirectoryPath, update, assemblyPath, usingNamespaces,
                templatePath, classNameTemplate)
        {
            _assemblyDtoPath = assemblyDtoPath;
            _ignoredNamespaces = ignoredNamespaces;
            _dtoNamespace = dtoNamespace;

            _ignoredProps = new Dictionary<string, IList<string>>();
        }

        /// <summary>
        /// If you want ignore some props for entity create add them by this methd. 
        /// </summary>
        /// <param name="props">List of ignored props</param>
        /// <param name="type">Type for ignored props. If you want add ignored props for all, leave default value</param>
        public void AddIgnoredProps(IList<string> props, string type = AllTypes)
        {
            if (_ignoredProps.ContainsKey(type))
                _ignoredProps.Remove(type);

            _ignoredProps.Add(type, props);
        }

        protected override string GetClassBody(string template, Type model)
        {
            var body = base.GetClassBody(template, model);
            var sb = new StringBuilder();
            var ignoredProps = GetIgnoredProps(model);

            var dtoType = Assembly.LoadFrom(_assemblyDtoPath).GetClassFromAssemblyNamespace(_dtoNamespace)
                .First(x => x.Name == $"{model.Name}Dto");

            foreach (var prop in dtoType.GetProperties())
            {
                if (ignoredProps.Contains(prop.Name))
                    continue;

                if (_ignoredNamespaces.Contains(prop.PropertyType.Namespace))
                    continue;

                sb.Append($"{prop.GetPropertyTypeName()} {prop.Name}, ");
            }

            sb = sb.Remove(sb.Length - 2, 2); // remove ", "

            return body
                .Replace(Consts.CreateMethodParams, sb.ToString());
        }

        private IList<string> GetIgnoredProps(Type model)
        {
            var ignoredProps = new List<string>();

            if (_ignoredProps.Count == 0)
                return ignoredProps;

            if (_ignoredProps.ContainsKey(AllTypes))
                ignoredProps.Union(_ignoredProps[AllTypes]);

            if (_ignoredProps.ContainsKey(model.Name))
                ignoredProps.Union(_ignoredProps[AllTypes]);

            return ignoredProps;
        }


    }
}
