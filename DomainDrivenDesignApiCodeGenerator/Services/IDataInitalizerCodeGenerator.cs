using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Services
{
    public class IDataInitalizerCodeGenerator : BaseClassCodeGenerator
    {
        public IDataInitalizerCodeGenerator(string dirPath, string @namespace, bool update) 
            : base(Path.Combine(dirPath, "IDataInitializer"), @namespace, Path.Combine("Services", "Templates", "IDataInitializerTemplate.txt"), update)
        {
        }
    }
}
