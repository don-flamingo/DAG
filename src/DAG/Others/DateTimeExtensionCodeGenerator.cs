using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class DateTimeExtensionCodeGenerator : BaseClassCodeGenerator
    {
        public DateTimeExtensionCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "DateTimeExtension"), @namespace, Path.Combine("Others", "Templates", "DateTimeExtension.txt"), update)
        {
        }
    }
}
