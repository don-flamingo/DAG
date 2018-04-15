using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Extensions;
using DomainDrivenDesignApiCodeGenerator.Helpers;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public class DomainServicesCodeGenerator : BaseClassesFromModelsAndDtosCodeGenerator
    {
        public DomainServicesCodeGenerator(string assemblyDtoPath, string assemblyPath, string usingNamespaces,
            string modelsNamepace, string generateClassesNamespace, string classDirectoryPath, string dtoNamespace,
            IList<string> ignoredNamespaces, bool update) : base(
            assemblyDtoPath, assemblyPath, usingNamespaces, modelsNamepace, generateClassesNamespace,
            classDirectoryPath, dtoNamespace, ignoredNamespaces, update, Path.Combine("Services", "Templates", "DomainServicesCodeGenerator.txt"), "{0}Service")
        {
        }

        protected override string GetClassBody(string template, Type model)
        {
            var sbMethodArgs = new StringBuilder();
            var sbEntityCtr = new StringBuilder();
            var sbLoggedParams = new StringBuilder();

            var ignoredProps = GetIgnoredProps(model);

            var dtoType = Assembly.LoadFrom(_assemblyDtoPath).GetClassFromAssemblyNamespace(_dtoNamespace)
                .First(x => x.Name == $"{model.Name}Dto");

            foreach (var prop in dtoType.GetProperties())
            {
                if (ignoredProps.Contains(prop.Name))
                    continue;

                if (prop.IsInNamespaces(_ignoredNamespaces.ToArray()))
                    continue;

                sbMethodArgs.Append($"{prop.GetPropertyTypeName()} {prop.Name.FirstLetterToLower()}, ");
                sbEntityCtr.AppendLine($"\t\t\t\t{prop.Name} = {prop.Name.FirstLetterToLower()},");
                sbLoggedParams.Append($"{{{prop.Name.FirstLetterToLower()}}} ");
            }

            sbMethodArgs = sbMethodArgs.Remove(sbMethodArgs.Length - 2, 2); // remove last ", "
            sbEntityCtr = sbEntityCtr.Remove(sbEntityCtr.Length - 1, 1); // remove last ","
            sbLoggedParams = sbLoggedParams.Remove(sbLoggedParams.Length - 1, 1); // remove last " "

            return template
                .Replace(Consts.ClassName, model.Name)
                .Replace(Consts.CreateMethodParamsLogged, sbLoggedParams.ToString())
                .Replace(Consts.ClassNameToLower, model.Name.FirstLetterToLower())
                .Replace(Consts.ClassBody, sbEntityCtr.ToString())
                .Replace(Consts.Namespaces, _usingNamespaces)
                .Replace(Consts.Namespace, _generateClassesNamespace)
                .Replace(Consts.CreateMethodParams, sbMethodArgs.ToString());
        }

        protected override void CreateBaseMarker()
            => new BaseDomainServiceCodeGenerator(_classDirectoryPath, _modelsNamepace, _update)
                .Generate();
    }

    public class BaseDomainServiceCodeGenerator : BaseClassCodeGenerator
    {
        public BaseDomainServiceCodeGenerator(string filePath, string @namespace ,bool update) :
            base(Path.Combine(filePath, "BaseDomainService"), @namespace, Path.Combine("Services", "Templates", "BaseDomainServiceCodeGenerator.txt"), update)
        {

        }
    }
}
