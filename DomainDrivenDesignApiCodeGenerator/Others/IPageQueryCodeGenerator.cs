using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class IPageQueryCodeGenerator : BaseClassCodeGenerator
    {
        public IPageQueryCodeGenerator(string filePath, string @namespace, bool update)
            : base(filePath, @namespace, Path.Combine("Others", "Templates", "IPageQueryTemplate.txt"), update, "")
        {
        }
    }
}
