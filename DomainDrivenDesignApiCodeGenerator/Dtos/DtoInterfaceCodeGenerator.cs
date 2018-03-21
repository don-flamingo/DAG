using System;
using System.Collections.Generic;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Dtos
{
    public class DtoInterfaceCodeGenerator : BaseDtoCodeGenerator
    {
        public DtoInterfaceCodeGenerator(string dtoNamespaceS, string dtosModelsPath, string modelsNamespace, string assemblyPath, bool isUpdate) : base(dtoNamespaceS, dtosModelsPath, modelsNamespace, assemblyPath, isUpdate)
        {
        }

        public override void Generate()
        {
            throw new NotImplementedException();
        }
    }
}
