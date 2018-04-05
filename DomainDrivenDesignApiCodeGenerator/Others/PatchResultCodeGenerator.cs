using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class PatchResultCodeGenerator : BaseClassCodeGenerator
    {
        public PatchResultCodeGenerator(string filePath, string @namespace, bool update, string dtosInterfaceNamespace)
            : base(Path.Combine(filePath, "PathResult"), @namespace, Path.Combine("Others", "Templates", "PathResultTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, dtosInterfaceNamespace);
        }
    }
}
