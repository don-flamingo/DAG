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
        protected readonly string _namespaces;
        protected readonly string _templatePath;
        protected readonly string _repositoryNameTemplate;
        protected readonly bool _update;

        protected BaseRepositoryCodeGenerator(string modelsNamepace, string repositoriesNamespace,
            string classDirectoryPath, bool update, string assemblyPath,
            string namespaces, string templatePath, string repositoryNameTemplate) : base(assemblyPath, classDirectoryPath)
        {
            _modelsNamepace = modelsNamepace;
            _repositoriesNamespace = repositoriesNamespace;
            _namespaces = namespaces;
            _update = update;
            _templatePath = templatePath;
            _repositoryNameTemplate = repositoryNameTemplate;
        }


        public override void Generate()
        {
            CreateBaseMarker();
            var models = GetModelsFromAssembly(_modelsNamepace);
            var template = ReadTemplate(_templatePath);

            foreach (var model in models)
            {
                var name = string.Format(_repositoryNameTemplate, model.Name);
                var body = GetClassBody(template)
                    .Replace(Consts.Classname, model.Name);

                CreateClass(Path.Combine(_classDirectoryPath, name), body, _update);
            }
        }

        protected abstract void CreateBaseMarker();

        protected virtual string GetClassBody(string template)
            => template.Replace(Consts.Namespace, _repositoriesNamespace)
                .Replace(Consts.Namespaces, _namespaces)
                .Replace(Consts.ModelsNamespace, _modelsNamepace);
    }
}

