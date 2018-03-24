using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public class EFRepositoryCodeGenerator : BaseRepositoryCodeGenerator
    {
        private readonly string _efContext;
        private readonly string _entityMarker;
        private readonly string _idProvider;

        public EFRepositoryCodeGenerator(string efContext, string entityMarker, string idProvider, string modelsNamepace, string repositoriesNamespace, string classDirectoryPath,
            bool update, string assemblyPath, string namespaces, string templatePath, string repositoryNameTemplate) :
            base(modelsNamepace, repositoriesNamespace, classDirectoryPath, update, assemblyPath, namespaces,
                templatePath, repositoryNameTemplate)
        {
            _efContext = efContext;
            _entityMarker = entityMarker;
            _idProvider = idProvider;
        }

        protected override void CreateBaseMarker()
            => new EFBaseRepositoryCodeGenerator(_efContext, _idProvider, _entityMarker, _classDirectoryPath, _repositoriesNamespace, _update)
                .Generate();

        private class EFBaseRepositoryCodeGenerator : BaseClassCodeGenerator
        {
            public EFBaseRepositoryCodeGenerator(string efContext, string idProvider, string entityMarker, string filePath, string @namespace, bool update, string assemblyPath = "")
                : base(filePath, @namespace, Path.Combine("Repositories", "Templates", "RepositoryBaseEFTemplate.txt"), update, assemblyPath)
            {
                AddBodyTemplateResolver(Consts.EFContext, efContext);
                AddBodyTemplateResolver(Consts.EntityMarker, entityMarker);
                AddBodyTemplateResolver(Consts.IdProvider, idProvider);
            }
        }
    }
}
