using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DomainDrivenDesignApiCodeGenerator
{
    public abstract class BaseClassCodeGenerator : BaseCodeGenerator
    {
        protected readonly string _namespace;
        protected readonly string _template;
        protected readonly string _filePath;
        protected readonly bool _update;

        //THIS PARAMTERTER IS NECESSARY FOR CORRECT BODY!
        private readonly IDictionary<string, string> _templateValues;

        protected BaseClassCodeGenerator(string filePath, string @namespace, string template, bool update, string assemblyPath = "") : base(assemblyPath, Path.GetDirectoryName(filePath))
        {
            _namespace = @namespace;
            _template = template;
            _update = update;
            _filePath = filePath;

            _templateValues = new Dictionary<string, string>
            {
                {Consts.Namespace, _namespace}
            };
        }

        public override void Generate()
        {
            var template = File.ReadAllText(_template);
            var body = CreateBody(template, _templateValues);
            var path = _filePath;

            if (!path.EndsWith(".g.cs"))
                path = $"{path}.g.cs";

            File.WriteAllText($"{path}", body);
            Console.WriteLine($"{path} created");
        }
    
        public void AddBodyTemplateResolver(string template, string value)
            => _templateValues.Add(template, value);
    }
}
