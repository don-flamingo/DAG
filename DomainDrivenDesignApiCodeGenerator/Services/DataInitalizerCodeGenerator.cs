using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Services
{
    public class DataInitalizerCodeGenerator : BaseClassCodeGenerator
    {
        public DataInitalizerCodeGenerator(string dirPath, string @namespace, bool update) 
            : base(Path.Combine(dirPath, "DataInitalizer"), @namespace, Path.Combine("Services", "Templates", "DataInitalizerTemplate.txt"), update)
        {
        }
    }
}
