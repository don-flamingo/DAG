using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class ObjectExtensionsCodeGenerator : BaseClassCodeGenerator
    {
        public ObjectExtensionsCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "ObjectExtensions"), @namespace, Path.Combine("Others", "Templates", "ObjectExtensionsTemplate.txt"), update)
        {
        }
    }
}
