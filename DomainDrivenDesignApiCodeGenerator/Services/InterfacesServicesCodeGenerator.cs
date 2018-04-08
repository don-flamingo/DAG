using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Helpers;
using DomainDrivenDesignApiCodeGenerator.Repositories;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public class InterfacesServicesCodeGenerator : BaseClassesFromModelsAndDtosCodeGenerator
    {
        public const string AllTypes = "$All";

        private readonly string _assemblyDtoPath;
        private readonly string _dtoNamespace;
        private readonly IList<string> _ignoredNamespaces;

        private readonly IDictionary<string, IList<string>> _ignoredProps;

        public InterfacesServicesCodeGenerator(string assemblyDtoPath, string assemblyPath, string usingNamespaces,
            string modelsNamepace, string generateClassesNamespace, string classDirectoryPath, string dtoNamespace,
            IList<string> ignoredNamespaces, bool update)
            : base(assemblyDtoPath, assemblyPath, usingNamespaces, modelsNamepace, generateClassesNamespace,
                classDirectoryPath, dtoNamespace, ignoredNamespaces, update,
                Path.Combine("Services", "Templates", "DomainServicesInterfacesTemplate.txt"), "I{0}Service")
        {
        }

        protected override void CreateBaseMarker()
            => new MarkerServiceCodeGenerator(_classDirectoryPath, _generateClassesNamespace, true)
                .Generate();

        private class MarkerServiceCodeGenerator : BaseClassCodeGenerator
        {
            public MarkerServiceCodeGenerator(string classPath, string @namespace, bool update,
                string assemblyPath = "")
                : base(Path.Combine(classPath, "IDomainService.cs"), @namespace,
                    Path.Combine("Services", "Templates", "IDomainServiceTemplate.txt"), update,
                    assemblyPath)
            {
            }
        }
    }
}
