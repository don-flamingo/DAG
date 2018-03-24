using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Repositories
{
  public class EFRepositoryCodeGenerator : BaseRepositoryCodeGenerator
  {
    private readonly string _idInterface;
    private readonly string _domainMarkerInterface;
    private readonly string _sortFuncNamespace;
    
    private readonly IList<Type> _models;

    public EFRepositoryCodeGenerator(string modelsNamepace, string repositoriesNamespace, string repositoriesPath,
      string repositoriesInterfacesPath, bool update, string assemblyPath, string idInterface,
      string domainMarkerInterface, string sortFuncNamespace) : base(modelsNamepace,
      repositoriesNamespace, repositoriesPath, repositoriesInterfacesPath, update, assemblyPath)
    {
      _idInterface = idInterface;
      _domainMarkerInterface = domainMarkerInterface;
      _sortFuncNamespace = sortFuncNamespace;

      _models = new List<Type>();
    }

    public override void Generate()
    {
      GenerateInterfaces();
      GenerateEFBaseRepositoryClass();
      GenerateAllRepositories();
    }

    public override void GenerateInterfaces()
    {
      throw new NotImplementedException();
    }

    private void GenerateAllRepositories()
    {
      throw new NotImplementedException();
    }

    private void GenerateEFBaseRepositoryClass()
    {
      throw new NotImplementedException();
    }
  }
}
