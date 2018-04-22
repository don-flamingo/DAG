using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Repositories;
using Humanizer;

namespace DomainDrivenDesignApiCodeGenerator.Controllers
{
    public class ControllersCodeGenerator : BaseClassesFromModelsCodeGenerator
    {
        private readonly string _commandsNamespace;
        public ControllersCodeGenerator(string modelsNamepace, string generateClassesNamespace,
            string classDirectoryPath, bool update, string assemblyPath, string usingNamespaces, string commandsNamespace) 
            : base(modelsNamepace, generateClassesNamespace, classDirectoryPath, update,
            assemblyPath, usingNamespaces, Path.Combine("Controllers", "Templates", "ControllerTemplate.txt"), "{0}Controller")
        {
                _commandsNamespace = commandsNamespace;
            _classNamesArePlurar = update;
        }

        protected override string GetClassBody(string template, Type model)
        {
            var body =  base.GetClassBody(template, model);
            var classNameToLower = model.Name.ToLower();
            var classNamePlurar = model.Name.Pluralize();
            var commandsNamespace = $"using {_commandsNamespace}.{model.Name};";

            return body.Replace(Consts.ClassNameToLower, classNameToLower)
                .Replace(Consts.PlurarClassName, classNamePlurar)
                .Replace(Consts.PlurarClassNameToLower, classNamePlurar.ToLower())
                .Replace(Consts.ANamespaces, commandsNamespace);
        }

        protected override void CreateBaseMarker()
            => new BaseApiControllerCodeGenerator(_classDirectoryPath, _generateClassesNamespace, _usingNamespaces,
                _update).Generate();

        private class BaseApiControllerCodeGenerator : BaseClassCodeGenerator
        {
            public BaseApiControllerCodeGenerator(string dirPath, string @namespace, string namespaces, bool update)
                : base(Path.Combine(dirPath, "BaseApiController"), @namespace, Path.Combine("Controllers", "Templates", "BaseControllerTemplate.txt"), update)
            {
                AddBodyTemplateResolver(Consts.Namespaces, namespaces);
            }
        }
    }

    
}
