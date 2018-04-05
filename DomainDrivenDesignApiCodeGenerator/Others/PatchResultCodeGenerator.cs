using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class PatchResultCodeGenerator : BaseClassCodeGenerator
    {
        public PatchResultCodeGenerator(string filePath, string @namespace, bool update, string dtosInterfaceNamespace)
            : base(Path.Combine(filePath, "PatchResult"), @namespace, Path.Combine("Others", "Templates", "PatchResultTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, dtosInterfaceNamespace);
        }
    }
}
