using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Services
{
    public class EncrypterCodeGenerator : BaseClassCodeGenerator
    {
        public EncrypterCodeGenerator(string filePath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(filePath, "Encrypter"), @namespace, Path.Combine("Services", "Templates", "EncrypterTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
