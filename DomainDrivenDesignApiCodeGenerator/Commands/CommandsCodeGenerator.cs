using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Extensions;
using DomainDrivenDesignApiCodeGenerator.Helpers;
using DomainDrivenDesignApiCodeGenerator.Repositories;

namespace DomainDrivenDesignApiCodeGenerator.Commands
{
    public class CommandsCodeGenerator : BaseClassesFromModelsAndDtosCodeGenerator
    {
        public CommandsCodeGenerator(string assemblyDtoPath, string assemblyPath, string usingNamespaces,
            string modelsNamepace, string generateClassesNamespace, string classDirectoryPath, string dtoNamespace, IList<string> ignoredNamespaces, bool update) : base(
            assemblyDtoPath, assemblyPath, usingNamespaces, modelsNamepace, generateClassesNamespace,
            classDirectoryPath, dtoNamespace, ignoredNamespaces, update, "", "")
        {
        }

        public override void Generate()
        {
            CreateBaseMarker();

            var models = GetModelsFromAssembly(_modelsNamepace);

            foreach (var model in models)
            {
                CreateCommandGenerate(model);
                UpdateCommandGenerate(model);
                DeleteCommandGenerate(model);
            }
        }

        private void CreateCommandGenerate(Type model)
        {
            var sbCommandBody = new StringBuilder();
            var ignoredProps = GetIgnoredProps(model);

            var dtoType = Assembly.LoadFrom(_assemblyDtoPath).GetClassFromAssemblyNamespace(_dtoNamespace)
                .First(x => x.Name == $"{model.Name}Dto");

            foreach (var prop in dtoType.GetProperties())
            {
                if (ignoredProps.Contains(prop.Name))
                    continue;

                if (prop.IsInNamespaces(_ignoredNamespaces.ToArray()))
                    continue;

                sbCommandBody.Append($"\t\t public {prop.GetPropertyTypeName()} {prop.Name} {{ get; set; }}{Environment.NewLine}");
            }

            var className = string.Format("Create{0}Command", model.Name);
            var folderName = model.Name;
            var namespaces = $"using Marvin.JsonPatch;{Environment.NewLine}" +
                             $"using {_dtoNamespace};";

            var body = GetCommandTemplateBody()
                .Replace(Consts.ClassName, className)
                .Replace(Consts.Namespace, $"{_generateClassesNamespace}.{model.Name}")
                .Replace(Consts.Body, sbCommandBody.ToString())
                .Replace(Consts.Interfaces, $": ICreateCommand<{model.Name}Dto>")
                .Replace(Consts.Namespaces, namespaces);

            CreateClass(Path.Combine(_classDirectoryPath, folderName, className), body, _update);
        }

        private void UpdateCommandGenerate(Type model)
        {
            var namespaces = $"using Marvin.JsonPatch;{Environment.NewLine}" +
                             $"using {_dtoNamespace};";

            var className = $"PartialUpdate{model.Name}Command";
            var usedInterfaces = $": IPartialUpdateCommand<{model.Name}Dto>";
            var updateCommandBody  = $"\t\tpublic Guid Id {{ get; set; }}\n" +
                $"\t\tpublic JsonPatchDocument<{model.Name}Dto> JsonPatchUpdate {{ get; set; }}\n" +
                "\t\tpublic Guid RequestBy { get; set; }";

            var folderName = model.Name;
            var body = GetCommandTemplateBody()
                .Replace(Consts.Namespace, $"{_generateClassesNamespace}.{model.Name}")
                .Replace(Consts.ClassName, className)
                .Replace(Consts.Interfaces, usedInterfaces)
                .Replace(Consts.Body, updateCommandBody)
                .Replace(Consts.Namespaces, namespaces);

            CreateClass(Path.Combine(_classDirectoryPath, folderName, className), body, _update);
        }

        private void DeleteCommandGenerate(Type model)
        {
            //throw new NotImplementedException();
        }

        public string GetCommandTemplateBody()
            => ReadTemplate(Path.Combine("Commands", "Templates", "CommandTemplate.txt"));


        protected override void CreateBaseMarker()
        {
            var updateCommandsNamespaces = $"using {_dtoNamespace};{Environment.NewLine}" +
                                           $"using {_dtoNamespace}.Interfaces;";

            new ICommandTemplateCodeGenerator(_classDirectoryPath, _generateClassesNamespace, _update).Generate();
            new IAuthenticatedCommandTemplateCodeGenerator(_classDirectoryPath, _generateClassesNamespace, _update).Generate();
            new IPartialUpdateCommandTemplateCodeGenerator(_classDirectoryPath, _generateClassesNamespace, updateCommandsNamespaces, _update).Generate();
            new ICreateCommandTemplateCodeGenerator(_classDirectoryPath, _generateClassesNamespace, updateCommandsNamespaces, _update).Generate();
        }
    }

    internal class ICommandTemplateCodeGenerator : BaseClassCodeGenerator
    {
        public ICommandTemplateCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "ICommand"), @namespace, Path.Combine("Commands", "Templates", "ICommandTemplate.txt"), update)
        {
        }
    }

    internal class IAuthenticatedCommandTemplateCodeGenerator : BaseClassCodeGenerator
    {
        public IAuthenticatedCommandTemplateCodeGenerator(string filePath, string @namespace, bool update)
            : base(Path.Combine(filePath, "IAuthenticatedCommand"), @namespace, Path.Combine("Commands", "Templates", "IAuthenticatedCommandTemplate.txt"), update)
        {
        }
    }

    internal class IPartialUpdateCommandTemplateCodeGenerator : BaseClassCodeGenerator
    {
        public IPartialUpdateCommandTemplateCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "IPartialUpdateCommand"), @namespace, Path.Combine("Commands", "Templates", "IPartialUpdateCommandTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }

    internal class ICreateCommandTemplateCodeGenerator : BaseClassCodeGenerator
    {
        public ICreateCommandTemplateCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "ICreateCommand"), @namespace, Path.Combine("Commands", "Templates", "ICreateCommandTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
