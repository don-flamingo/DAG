using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class PredicateHelperCodeGenerator : BaseClassCodeGenerator
    {
        public PredicateHelperCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "PredicateHelper"), @namespace, Path.Combine("Others", "Templates", "PredicateHelperTemplate.txt"), update)
        {
        }
    }
}
