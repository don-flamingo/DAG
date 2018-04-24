using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DAG;
using DAG.Extensions;
using DAG.Helpers;
using DAG.Repositories;

namespace DAG.Commands
{
    public class CommandsHandlersCodeGenerator : BaseClassesFromModelsCodeGenerator
    {
        private readonly string _commandsNamespace;
        private readonly string _modelsNamespace;
        private readonly string _commandsAssemblyPath;


        public CommandsHandlersCodeGenerator(string commandsNamespace, string generateClassesNamespace,
            string classDirectoryPath, bool update, string commandsAssemblyPath, string usingNamespaces, string modelsNamespace, string modelsAssembly) : base(modelsNamespace, generateClassesNamespace, classDirectoryPath, update,
            modelsAssembly, usingNamespaces, Path.Combine("Commands", "Templates", "CommandHandlerTemplate.txt"), "{0}Handler")
        {
            _commandsNamespace = commandsNamespace;
            _commandsAssemblyPath = commandsAssemblyPath;
        }

        protected override void CreateBaseMarker()
        {
            new ICommandHandlerCodeGenerator(_classDirectoryPath, _generateClassesNamespace, $"using {_commandsNamespace};", _update).Generate();
        }

        protected override string GetClassBody(string template, Type model)
        {
            var sbCreateCommandBody = new StringBuilder();
            var body = base.GetClassBody(template, model);
            var command = GetModelsFromAssembly(_commandsAssemblyPath, $"{_commandsNamespace}.{model.Name}")
                .First(x => x.Name.StartsWith("Create"));

            foreach (var commandProperty in command.GetProperties())
            {
                sbCreateCommandBody.Append($"command.{commandProperty.Name}, ");
            }

            sbCreateCommandBody = sbCreateCommandBody.Remove(sbCreateCommandBody.Length - 2, 2); // remove last ", "

            return body.Replace(Consts.CreateMethodParams, sbCreateCommandBody.ToString())
                .Replace(Consts.ClassNameToLower, model.Name.FirstLetterToLower())
                .Replace(Consts.ANamespaces, $"using {_commandsNamespace}.{model.Name};");
        }
    }

    internal class ICommandHandlerCodeGenerator : BaseClassCodeGenerator
    {
        public ICommandHandlerCodeGenerator(string filePath, string @namespace, string namespaces, bool update) : base(
            Path.Combine(filePath, "ICommandHandler"), @namespace,
            Path.Combine("Commands", "Templates", "ICommandHandlerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }

}
