using System;
using System.Collections.Generic;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
    public abstract class BaseRepositoryCodeGenerator : BaseCodeGenerator
    {
        private readonly string _modelsNamepace;
        private readonly string _repositoriesNamespace;
        private readonly string _repositoriesPath;
        private readonly string _repositoriesInterfacesPath;
        private readonly bool _update;

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
