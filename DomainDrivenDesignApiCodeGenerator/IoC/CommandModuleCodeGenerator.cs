using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.IoC
{
    public class CommandModuleCodeGenerator : BaseClassCodeGenerator
    {
        public CommandModuleCodeGenerator(string dirPath, string @namespace, string namespaces, bool update) 
            : base(Path.Combine(dirPath, "CommandModule"), @namespace, Path.Combine("IoC", "Templates", "CommandModuleTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
