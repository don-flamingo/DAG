using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Services
{
    public class MailerCodeGenerator : BaseClassCodeGenerator
    {
        public MailerCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "Mailer"), @namespace, Path.Combine("Services", "Templates", "MailerTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
