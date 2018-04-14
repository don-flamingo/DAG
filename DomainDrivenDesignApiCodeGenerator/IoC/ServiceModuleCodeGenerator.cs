using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.IoC
{
    public class ServiceModuleCodeGenerator : BaseClassCodeGenerator
    {
        public ServiceModuleCodeGenerator(string dirPath, string @namespace, string namespaces, string efContext, bool update)
            : base(Path.Combine(dirPath, "ServiceModule"), @namespace,
                Path.Combine("IoC", "Templates", "ServiceModuleTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
            AddBodyTemplateResolver(Consts.EFContext, efContext);

        }
    }
}
