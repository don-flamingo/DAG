using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public abstract class BaseRepositoryCodeGenerator : BaseCodeGenerator
    {
        protected readonly string _modelsNamepace;
        protected readonly string _repositoriesNamespace;
        protected readonly string _repositoriesPath;
        protected readonly string _namespaces;
        protected readonly string _templatePath;
        protected readonly string _repositoryNameTemplate;
        protected readonly bool _update;

        protected BaseRepositoryCodeGenerator(string modelsNamepace, string repositoriesNamespace,
            string repositoriesPath, bool update, string assemblyPath,
            string namespaces, string templatePath, string repositoryNameTemplate) : base(assemblyPath)
        {
            _modelsNamepace = modelsNamepace;
            _repositoriesNamespace = repositoriesNamespace;
            _repositoriesPath = repositoriesPath;
            _namespaces = namespaces;
            _update = update;
            _templatePath = templatePath;
            _repositoryNameTemplate = repositoryNameTemplate;
        }


        public override void Generate()
        {
            CreateMarkerInterface();
            var models = GetModelsFromAssembly(_modelsNamepace);
            var template = ReadTemplate(_templatePath);

            foreach (var model in models)
            {
                var name = string.Format(_repositoryNameTemplate, model.Name);
                var body = template.Replace(Consts.Namespace, _repositoriesNamespace)
                    .Replace(Consts.Classname, model.Name)
                    .Replace(Consts.Namespaces, _namespaces)
                    .Replace(Consts.ModelsNamespace, _modelsNamepace);

                CreateClass(Path.Combine(_repositoriesPath, name), body, _update);
            }
        }

        protected abstract void CreateMarkerInterface();
    }
}

