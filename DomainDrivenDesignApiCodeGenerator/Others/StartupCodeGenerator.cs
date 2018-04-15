using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class StartupCodeGenerator : BaseClassCodeGenerator
    {
        public StartupCodeGenerator(string dirPath, string @namespace, string efContext, string namespaces, string appName, bool update) 
            : base(Path.Combine(dirPath, "Startup"), @namespace, Path.Combine("Others", "Templates", "StartupTemplate.txt"), update)
        {
            _gExt = false;
            
            AddBodyTemplateResolver(Consts.EFContext, efContext);
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
            AddBodyTemplateResolver(Consts.AppName, appName);

        }
    }
}
