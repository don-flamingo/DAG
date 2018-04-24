using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.IoC
{
    public class ContainerModuleCodeGenerator : BaseClassCodeGenerator
    {
        public ContainerModuleCodeGenerator(string dirPath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(dirPath, "ContainerModule"), @namespace, Path.Combine("IoC", "Templates", "ContainerModuleTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
