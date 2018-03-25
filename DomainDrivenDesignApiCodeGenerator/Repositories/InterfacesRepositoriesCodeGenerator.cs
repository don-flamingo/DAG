using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public class InterfaceRepositoryCodeGenerator : BaseRepositoryCodeGenerator
    {

        public InterfaceRepositoryCodeGenerator(string modelsNamepace, string repositoriesNamespace,
            string classDirectoryPath, bool update, string assemblyPath, string namespaces) 
            : base(modelsNamepace, repositoriesNamespace, classDirectoryPath, update,
                assemblyPath, namespaces, Path.Combine("Repositories", "Templates", "RepositoryInterfaceTemplate.txt"), "I{0}Repository")
        {
        }

        protected override void CreateBaseMarker()
            => new MarkerRepositoryCodeGenerator(_classDirectoryPath, _repositoriesNamespace, _update, _assemblyPath)
                .Generate();


        private class MarkerRepositoryCodeGenerator : BaseClassCodeGenerator
        {
            public MarkerRepositoryCodeGenerator(string classPath, string @namespace, bool update,
                string assemblyPath = "")
                : base(Path.Combine(classPath, "IRepository.cs"), @namespace,
                    Path.Combine("Repositories", "Templates", "RepositoryMarkerInterfaceTemplate.txt"), update,
                    assemblyPath)
            {
            }
        }
    }
}
