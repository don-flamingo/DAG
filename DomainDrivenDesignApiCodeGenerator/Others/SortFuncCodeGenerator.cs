using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class SortFuncCodeGenerator : BaseClassCodeGenerator
    {
        public SortFuncCodeGenerator(string classPath, string @namespace, bool update) 
            : base(Path.Combine(classPath, "SortFunc"), @namespace, Path.Combine("Others","Templates", "SortFuncTemplate.txt"), update)
        {

        }
    }
}
