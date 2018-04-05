using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class PageQueryCodeGenerator : BaseClassCodeGenerator
    {
        public PageQueryCodeGenerator(string filePath, string @namespace, string usingNamespaces, bool update)
            : base(filePath, @namespace, Path.Combine("Others", "Templates", "PageQueryTemplate.txt"), update, "")
        {
            AddBodyTemplateResolver(Consts.Namespaces, usingNamespaces);
        }
    }
}
