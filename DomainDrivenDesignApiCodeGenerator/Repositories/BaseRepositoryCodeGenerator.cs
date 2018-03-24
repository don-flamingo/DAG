using System;
using System.Collections.Generic;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public abstract class BaseRepositoryCodeGenerator : BaseCodeGenerator
    {
        protected readonly string _modelsNamepace;
        protected readonly string _repositoriesNamespace;
        protected readonly string _repositoriesPath;
        protected readonly string _repositoriesInterfacesPath;
        protected readonly bool _update;

        protected BaseRepositoryCodeGenerator(string modelsNamepace, string repositoriesNamespace, string repositoriesPath, string repositoriesInterfacesPath, bool update, string assemblyPath) : base(assemblyPath)
        {
            _modelsNamepace = modelsNamepace;
            _repositoriesNamespace = repositoriesNamespace;
            _repositoriesPath = repositoriesPath;
            _repositoriesInterfacesPath = repositoriesInterfacesPath;
            _update = update;
        }

        public abstract void GenerateInterfaces();
    }
}
