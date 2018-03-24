using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class AutoMapperConfigCodeGenerator : BaseClassCodeGenerator
    {
        private readonly string _lineTemplate;
        private readonly string _namespaces;
        private readonly string _modelsNamespace;
        public AutoMapperConfigCodeGenerator(string namespaces, string modelsNamespace, string filePath, string @namespace, bool update, string assemblyPath) 
            : base(filePath, @namespace, Path.Combine("Others", "Templates", "AutoMapperConfigTemplate.txt"), update, assemblyPath)
        {
            _lineTemplate = Path.Combine("Others", "Templates", "AutoMapperConfigLineTemplate.txt");
            _namespaces = namespaces;
            _modelsNamespace = modelsNamespace;
        }

        public override void Generate()
        {
            var models = GetModelsFromAssembly(_modelsNamespace);
            var configTemplate = ReadTemplate(_template);
            var lineTemplate = ReadTemplate(_lineTemplate);
            var sb = new StringBuilder();

            foreach (var model in models)
            {
                sb.Append(lineTemplate.Replace(Consts.Classname, model.Name) + Environment.NewLine);
            }

            var body = configTemplate.Replace(Consts.Namespaces, _namespaces)
                .Replace(Consts.Namespace, _namespace)
                .Replace(Consts.Body, sb.ToString());

            CreateClass(_filePath, body, _update);
        }
    }
}
