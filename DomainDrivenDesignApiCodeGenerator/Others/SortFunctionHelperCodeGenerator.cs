using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class SortFunctionHelperCodeGenerator : BaseClassCodeGenerator
    {
        public SortFunctionHelperCodeGenerator(string filePath, string @namespace, string usingNamespaces, bool update)
            : base(filePath, @namespace, Path.Combine("Others", "Templates", "SortFuncHelperTemplate.txt") , update, "")
        {
            AddBodyTemplateResolver(Consts.Namespaces, usingNamespaces);
        }
    }
}
