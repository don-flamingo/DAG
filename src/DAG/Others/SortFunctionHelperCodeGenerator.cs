using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class SortFunctionHelperCodeGenerator : BaseClassCodeGenerator
    {
        public SortFunctionHelperCodeGenerator(string filePath, string @namespace, string usingNamespaces, bool update)
            : base(Path.Combine(filePath, "NavigationFunctionHelper"), @namespace, Path.Combine("Others", "Templates", "SortFuncHelperTemplate.txt") , update, "")
        {
            AddBodyTemplateResolver(Consts.Namespaces, usingNamespaces);
        }
    }
}
