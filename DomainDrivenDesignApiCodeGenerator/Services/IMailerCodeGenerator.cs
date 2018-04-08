using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public class IMailerCodeGenerator : BaseClassCodeGenerator
    {
        public IMailerCodeGenerator(string filePath, string @namespace, string namespaces, bool update) 
            : base(Path.Combine(filePath, "IMailer"), @namespace, Path.Combine("Services", "Templates", "IMailerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
