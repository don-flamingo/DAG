using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Services
{
    public class IJwtHandlerCodeGenerator : BaseClassCodeGenerator
    {
        public IJwtHandlerCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "IJwtHandler"), @namespace, Path.Combine("Services", "Templates", "IJwtHandlerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}

