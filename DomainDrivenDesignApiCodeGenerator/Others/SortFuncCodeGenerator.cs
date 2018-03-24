using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class SortFuncCodeGenerator : BaseClassCodeGenerator
    {
        public SortFuncCodeGenerator(string classPath, string @namespace, bool update) 
            : base(Path.Combine(classPath, "SortFunc.g.cs"), @namespace, Path.Combine("Others","Templates", "SortFuncTemplate.txt"), update)
        {

        }
    }
}
