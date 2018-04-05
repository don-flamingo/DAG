using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public class EFRepositoriesCodeGenerator : BaseClassesFromModelsCodeGenerator
    {
        private readonly string _efContext;
        private readonly string _entityMarker;
        private readonly string _idProvider;

        public EFRepositoriesCodeGenerator(string efContext, string entityMarker, string idProvider, string modelsNamepace, string generateClassesNamespace, string classDirectoryPath,
            bool update, string assemblyPath, string usingNamespaces) :
            base(modelsNamepace, generateClassesNamespace, classDirectoryPath, update, assemblyPath, usingNamespaces,
                Path.Combine("Repositories", "Templates", "RepositoryEFTemplate.txt"), "{0}Repository")
        {
            _efContext = efContext;
            _entityMarker = entityMarker;
            _idProvider = idProvider;
        }

        protected override string GetClassBody(string template, Type model)
        {
            var includeTemplate = ".Include(x => x.{0})";
            var body = base.GetClassBody(template, model);
            var stringBuilder = new StringBuilder();
            var enitityModels = model.GetProperties().Where(x => x.PropertyType.Namespace == _modelsNamepace 
                || (x.PropertyType.IsGenericType && x.PropertyType.GenericTypeArguments[0].Namespace == _modelsNamepace));

            foreach (var entityModel in enitityModels)
            {
                stringBuilder.AppendFormat(includeTemplate, entityModel.Name);
            }

            body = body.Replace(Consts.Inlcudes, stringBuilder.ToString())
                .Replace(Consts.EFContext, _efContext);
            
            return body;
        }

        protected override void CreateBaseMarker()
            => new EFBaseRepositoryCodeGenerator(_efContext, _idProvider, _entityMarker, _usingNamespaces, Path.Combine(_classDirectoryPath, "BaseEfRepository"), _generateClassesNamespace, _update)
                .Generate();

        private class EFBaseRepositoryCodeGenerator : BaseClassCodeGenerator
        {
            public EFBaseRepositoryCodeGenerator(string efContext, string idProvider, string entityMarker, string namespaces, string filePath, string @namespace, bool update, string assemblyPath = "")
                : base(filePath, @namespace, Path.Combine("Repositories", "Templates", "RepositoryBaseEFTemplate.txt"), update, assemblyPath)
            {
                AddBodyTemplateResolver(Consts.EFContext, efContext);
                AddBodyTemplateResolver(Consts.EntityMarker, entityMarker);
                AddBodyTemplateResolver(Consts.IdProvider, idProvider);
                AddBodyTemplateResolver(Consts.Namespaces, namespaces );
            }
        }
    }
}
