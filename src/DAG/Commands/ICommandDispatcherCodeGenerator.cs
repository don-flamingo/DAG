using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Commands
{
    public class ICommandDispatcherCodeGenerator : BaseClassCodeGenerator
    {
        public ICommandDispatcherCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "ICommandDispatcher"), @namespace, Path.Combine("Commands", "Templates", "ICommandDispatcherTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
