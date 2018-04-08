using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public class JwtHandlerCodeGenerator : BaseClassCodeGenerator
    {
        public JwtHandlerCodeGenerator(string filePath, string @namespace, string namespaces, bool update) 
            : base(Path.Combine(filePath, "JwtHandler"), @namespace, Path.Combine("Services", "Templates", "JwtHandlerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
