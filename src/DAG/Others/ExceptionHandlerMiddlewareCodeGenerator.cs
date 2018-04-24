using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class ExceptionHandlerMiddlewareCodeGenerator : BaseClassCodeGenerator
    {
        public ExceptionHandlerMiddlewareCodeGenerator(string dirPath, string @namespace, string appName, string namespaces, bool update) 
            : base(Path.Combine(dirPath, "ExceptionHandlerMiddleware"), @namespace, Path.Combine("Others", "Templates", "ExceptionHandlerMiddlewareTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.AppName, appName);
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
