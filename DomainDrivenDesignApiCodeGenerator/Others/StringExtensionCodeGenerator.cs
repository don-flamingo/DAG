using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class StringExtensionCodeGenerator : BaseClassCodeGenerator
    {
        public StringExtensionCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "StringExtension"), @namespace, Path.Combine("Others", "Templates", "StringExtensionTemplate.txt"), update)
        {
        }
    }
}
