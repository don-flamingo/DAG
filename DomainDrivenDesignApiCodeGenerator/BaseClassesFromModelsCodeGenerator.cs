using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Humanizer;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public abstract class BaseClassesFromModelsCodeGenerator : BaseCodeGenerator
    {
        protected readonly string _modelsNamepace;
        protected readonly string _generateClassesNamespace;
        protected readonly string _usingNamespaces;
        protected readonly string _templatePath;
        protected readonly string _classNameTemplate;
        protected readonly bool _update;

        protected bool _classNamesArePlurar;

        protected BaseClassesFromModelsCodeGenerator(string modelsNamepace, string generateClassesNamespace,
            string classDirectoryPath, bool update, string assemblyPath,
            string usingNamespaces, string templatePath, string classNameTemplate) : base(assemblyPath, classDirectoryPath)
        {
            _modelsNamepace = modelsNamepace;
            _generateClassesNamespace = generateClassesNamespace;
            _usingNamespaces = usingNamespaces;
            _update = update;
            _templatePath = templatePath;
            _classNameTemplate = classNameTemplate;
        }


        public override void Generate()
        {
            CreateBaseMarker();
            var models = GetModelsFromAssembly(_modelsNamepace);
            var template = ReadTemplate(_templatePath);

            foreach (var model in models)
            {
                var name = string.Format(_classNameTemplate, model.Name);
                var body = GetClassBody(template, model);

                if (_classNamesArePlurar)
                    name = string.Format(_classNameTemplate, model.Name.Pluralize());

                CreateClass(Path.Combine(_classDirectoryPath, name), body, _update);
            }
        }

        protected abstract void CreateBaseMarker();

        /// <summary>
        /// Override this method for correctly class body.
        /// </summary>
        /// <param name="template">Read string template (not patch to template)</param>
        /// <param name="model">Entity model type, ex. DTO, POCO</param>
        /// <returns></returns>
        protected virtual string GetClassBody(string template, Type model)
        {
            var body = template.Replace(Consts.Namespace, _generateClassesNamespace)
                .Replace(Consts.Namespaces, _usingNamespaces)
                .Replace(Consts.ModelsNamespace, _modelsNamepace);

            if(model != null)
                body = body.Replace(Consts.ClassName, model.Name);

            return body;
        }
    }
}

