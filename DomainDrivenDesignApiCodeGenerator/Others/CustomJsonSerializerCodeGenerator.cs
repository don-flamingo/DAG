using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class CustomJsonSerializerCodeGenerator : BaseClassCodeGenerator
    {
        public CustomJsonSerializerCodeGenerator(string dirPath, string @namespace, string appName, bool update)
            : base(Path.Combine(dirPath, $"{appName}JsonSerializer"), @namespace, Path.Combine("Others", "Templates", "CustomJsonSerializerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.AppName, appName);
        }
    }
}
