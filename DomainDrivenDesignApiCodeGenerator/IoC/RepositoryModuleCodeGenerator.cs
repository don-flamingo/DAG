using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.IoC
{
    public class RepositoryModuleCodeGenerator : BaseClassCodeGenerator
    {
        public RepositoryModuleCodeGenerator(string dirPath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(dirPath, "RepositoryModule"), @namespace, Path.Combine("IoC", "Templates", "RepositoryModuleTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }

    }
}
