using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public class InterfaceRepositoryCodeGenerator : BaseRepositoryCodeGenerator
    {
        private readonly string _sortFuncNamespace;
        public InterfaceRepositoryCodeGenerator(string sortFuncNamespace, string modelsNamepace, string repositoriesNamespace, string repositoriesPath, bool update, string assemblyPath) : base(modelsNamepace, repositoriesNamespace, repositoriesPath, update, assemblyPath)
        {
            _sortFuncNamespace = sortFuncNamespace;
        }

        public override void Generate()
        {
            CreateMarkerInterface();
            var models = GetModelsFromAssembly(_modelsNamepace);
            var template = ReadTemplate(Path.Combine("Repositories", "Templates", "RepositoryInterfaceTemplate.txt"));

            foreach (var model in models)
            {
                var body = template.Replace(Consts.Namespace, _repositoriesNamespace)
                    .Replace(Consts.Classname, model.Name)
                    .Replace(Consts.SortFuncNamespace, _sortFuncNamespace)
                    .Replace(Consts.ModelsNamespace, _modelsNamepace);

                CreateClass(Path.Combine(_repositoriesPath, $"I{model.Name}Repository"), body, _update);
            }
        }

        private void CreateMarkerInterface()
            => new MarkerRepositoryCodeGenerator(_repositoriesPath, _repositoriesNamespace, _update, _assemblyPath)
                .Generate();
    }

    internal class MarkerRepositoryCodeGenerator : BaseClassCodeGenerator
    {
        public MarkerRepositoryCodeGenerator(string classPath, string @namespace, bool update, string assemblyPath = "") 
            : base(Path.Combine(classPath, "IRepository.cs"), @namespace, Path.Combine("Repositories", "Templates", "RepositoryMarkerInterfaceTemplate.txt"), update, assemblyPath)
        {
        }
    }
}
