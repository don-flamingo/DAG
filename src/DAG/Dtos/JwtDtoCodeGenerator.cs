using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Dtos
{
    public class JwtDtoCodeGenerator : BaseClassCodeGenerator
    {
        public JwtDtoCodeGenerator(string filePath, string @namespace, string namespaces, bool update) 
            : base(Path.Combine(filePath, "JwtDto"), @namespace, Path.Combine("Dtos", "JwtDtoTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
