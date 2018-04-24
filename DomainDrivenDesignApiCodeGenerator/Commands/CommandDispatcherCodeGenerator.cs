using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Commands
{
    public class CommandDispatcherCodeGenerator : BaseClassCodeGenerator
    {
        public CommandDispatcherCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "CommandDispatcher"), @namespace, Path.Combine("Commands", "Templates", "CommandsDispatcherTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
