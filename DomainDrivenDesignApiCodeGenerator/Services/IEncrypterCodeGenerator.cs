using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public class IEncrypterCodeGenerator : BaseClassCodeGenerator
    {
        public IEncrypterCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "IEncrypter"), @namespace, Path.Combine("Services", "Templates", "IEncrypterTemplate.txt"), update)
        {
        }
    }
}
