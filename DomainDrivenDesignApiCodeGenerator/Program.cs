using System;
using DomainDrivenDesignApiCodeGenerator.Dtos;
using DomainDrivenDesignApiCodeGenerator.Interfaces;
using DomainDrivenDesignApiCodeGenerator.Others;
using DomainDrivenDesignApiCodeGenerator.Repositories;

namespace DomainDrivenDesignApiCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dtoNamespace = "Gymmer.Framework.Dtos";
            var modelsNamespace = "Gymmer.Server.Core.Models";
            var dtoPath = @"D:\Codes\My\Gymmer\src\Gymmer.Framework\Dtos";
            var modelsPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Models";
            var assembly =
                @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\bin\Debug\netcoreapp2.0\Gymmer.Server.Core.dll";
            var dtoAssembly = @"D:\Codes\My\Gymmer\src\Gymmer.Framework\bin\Debug\netstandard2.0\Gymmer.Framework.dll";

            var sortFuncPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Framework";
            var sortFuncNamespace = "Gymmer.Server.Core.Framework";

            var repositoryNamespace = "Gymmer.Server.Core.Repositories";
            var repostioryPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Repositories";
            var interfaceRepositoryNamespaces = $"using Gymmer.Server.Core.Framework; {Environment.NewLine}" +
                                                $"using Gymmer.Server.Core.Models;";

            var entityFrameworkRepositoryNamespace = "Gymmer.Server.Infrastructure.Repositories.EF";
            var efRepositoryPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Infrastructure\Repositories\EF";
            var context = "GymmerContext";
            var entityMarker = "IGymmerObject";
            var idProvider = "IIdProvider";
            var efRepoNamespaces = $"using Gymmer.Server.Core.Framework; {Environment.NewLine}" +
                                   $"using Gymmer.Server.Core.Models; {Environment.NewLine}" +
                                   $"using Gymmer.Server.Core.Models.Interfaces; {Environment.NewLine}" +
                                   $"using Gymmer.Server.Core.Repositories; {Environment.NewLine}";

            var mapperPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Infrastructure\Mappers\AutoMapperConfig";
            var mapperMamespace = "Gymmer.Server.Infrastructure.Mappers";
            var mapperNamespaces = $"using {modelsNamespace}; {Environment.NewLine}" +
                                   $"using {dtoNamespace}; {Environment.NewLine}";

                                   
            var dtoCodeGenerator = new DtoCodeGenerator(dtoNamespace, dtoPath, modelsNamespace, assembly, true);
            dtoCodeGenerator.Generate();

            var interfaceGenerator =
                new InterfaceCodeGenerator(modelsPath, modelsNamespace, assembly, true, 3, "IGymmerObject", postfix: "Provider");
            interfaceGenerator.Generate();

            var dtoInterfaceGenerator =
                new InterfaceCodeGenerator(dtoPath, dtoNamespace, dtoAssembly, true, 3, "IDto", "Dto");
            dtoInterfaceGenerator.Generate();

            var sortFuncGenerator = new SortFuncCodeGenerator(sortFuncPath, sortFuncNamespace, true);
            sortFuncGenerator.Generate();

            var interfaceRepositoryCodeGenerator = new InterfaceRepositoryCodeGenerator(modelsNamespace,
                repositoryNamespace, repostioryPath, true, assembly, interfaceRepositoryNamespaces);
            interfaceRepositoryCodeGenerator.Generate();

            var efRepositoryCodeGenerator = new EFRepositoryCodeGenerator(context, entityMarker, idProvider, modelsNamespace, entityFrameworkRepositoryNamespace, efRepositoryPath, true, assembly, efRepoNamespaces);
            efRepositoryCodeGenerator.Generate();

            var mapperCodeGenerator = new AutoMapperConfigCodeGenerator(mapperNamespaces,  modelsNamespace,mapperPath, mapperMamespace, true, assembly);
            mapperCodeGenerator.Generate();

            Console.WriteLine("End.");

            Console.ReadLine();

        }
    }
}
