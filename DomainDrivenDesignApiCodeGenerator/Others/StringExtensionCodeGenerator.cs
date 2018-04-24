using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class StringExtensionCodeGenerator : BaseClassCodeGenerator
    {
        public StringExtensionCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "StringExtensions"), @namespace, Path.Combine("Others", "Templates", "StringExtensionTemplate.txt"), update)
        {
        }
    }
}
