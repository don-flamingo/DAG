using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Services
{
    public class JwtHandlerCodeGenerator : BaseClassCodeGenerator
    {
        public JwtHandlerCodeGenerator(string filePath, string @namespace, string namespaces, string appName, bool update) 
            : base(Path.Combine(filePath, "JwtHandler"), @namespace, Path.Combine("Services", "Templates", "JwtHandlerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
            AddBodyTemplateResolver(Consts.AppName, appName);
        }
    }
}
