using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class CacheExtensionCodeGenerator : BaseClassCodeGenerator
    {
        public CacheExtensionCodeGenerator(string filePath, string @namespace, string namespaces, bool update) 
            : base(Path.Combine(filePath, "CacheExtensions"), @namespace, Path.Combine("Others", "Templates", "CacheExtensionHelper.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
