using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Dtos
{
    public abstract class BaseDtoCodeGenerator
    {
        protected readonly string _dtoNamespace;
        protected readonly string _dtosModelsPath;
        protected readonly string _modelsNamespace;
        protected readonly string _assemblyPath;
        protected readonly bool _isUpdate;
        protected BaseDtoCodeGenerator(string dtoNamespaceS, string dtosModelsPath, string modelsNamespace, string assemblyPath, bool isUpdate)
        {
            _dtoNamespace = dtoNamespaceS;
            _dtosModelsPath = dtosModelsPath;
            _modelsNamespace = modelsNamespace;
            _assemblyPath = assemblyPath;
            _isUpdate = isUpdate;
        }

        public abstract void Generate();
    }
}
