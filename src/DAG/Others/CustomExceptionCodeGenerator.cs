using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class CustomExceptionCodeGenerator : BaseClassCodeGenerator
    {
        public CustomExceptionCodeGenerator(string dirPath, string @namespace, string appName, bool update, string assemblyPath = "") 
            : base(Path.Combine(dirPath, $"{appName}Exception"), @namespace, Path.Combine("Others", "Templates", "CustomExceptionTemplate.txt"), update, assemblyPath)
        {
            AddBodyTemplateResolver(Consts.AppName, appName);
        }
    }
}
