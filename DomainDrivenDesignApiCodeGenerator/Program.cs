using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Threading;
using DomainDrivenDesignApiCodeGenerator.Dtos;
using DomainDrivenDesignApiCodeGenerator.Interfaces;
using DomainDrivenDesignApiCodeGenerator.Others;
using DomainDrivenDesignApiCodeGenerator.Repositories;
using DomainDrivenDesignApiCodeGenerator.Services;

namespace DomainDrivenDesignApiCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainCodePath = @"D:\Codes\My\Gymmer\src";
            var projectName = "Gymmer";

            var CommonNamespace = "Gymmer.Common";
            var CommonCodePath = Path.Combine(mainCodePath, CommonNamespace);
            var commonExtensionsPath = Path.Combine(CommonCodePath, "Extensions");
            var commonExtensionsNamespace = $"{CommonNamespace}.Extensions";

            var infrastrucutreNamespace = $"{projectName}.Server.Infrastructure";
            var infrastracturePath = Path.Combine(mainCodePath, $"{projectName}.Server",
                infrastrucutreNamespace);
            
            

            var dtoNamespace = "Gymmer.Common.Dtos";
            var modelsNamespace = "Gymmer.Server.Core.Models";
            var dtoPath = @"D:\Codes\My\Gymmer\src\Gymmer.Common\Dtos";
            var modelsPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Models";
            var assembly =
                @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\bin\Debug\netcoreapp2.0\Gymmer.Server.Core.dll";
            var dtoAssembly = @"D:\Codes\My\Gymmer\src\Gymmer.Common\bin\Debug\netstandard2.0\Gymmer.Common.dll";

            var sortFuncPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Common";
            var sortFuncNamespace = "Gymmer.Server.Core.Common";

            var repositoryNamespace = "Gymmer.Server.Core.Repositories";
            var repostioryPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Repositories";
            var interfaceRepositoryNamespaces = $"using Gymmer.Server.Core.Common; {Environment.NewLine}" +
                                                $"using Gymmer.Server.Core.Models;";

            var entityCommonRepositoryNamespace = "Gymmer.Server.Infrastructure.Repositories.EF";
            var efRepositoryPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Infrastructure\Repositories\EF";
            var context = "GymmerContext";
            var entityMarker = "IGymmerObject";
            var idProvider = "IIdProvider";
            var efRepoNamespaces = $"using Gymmer.Server.Core.Common; {Environment.NewLine}" +
                                   $"using Gymmer.Server.Core.Models; {Environment.NewLine}" +
                                   $"using Gymmer.Server.Core.Models.Interfaces; {Environment.NewLine}" +
                                   $"using Gymmer.Server.Core.Repositories; {Environment.NewLine}";

            var mapperPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Infrastructure\Mappers\AutoMapperConfig";
            var mapperMamespace = "Gymmer.Server.Infrastructure.Mappers";
            var mapperNamespaces = $"using {modelsNamespace}; {Environment.NewLine}" +
                                   $"using {dtoNamespace}; {Environment.NewLine}";

            var ipageQueryPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Infrastructure\Models\Interfaces\IPageQuery";
            var iPageQueryNamesapce = "Gymmer.Server.Infrastructure.Models.Interface";

            var pageQueryPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Api\Models\PageQuery";
            var pageQueryNamesapce = "Gymmer.Server.Api.Models";
            var pageQueryNamespaces = $"using Gymmer.Server.Infrastructure.Models.Interface;";

            var iServiceNamespace = "Gymmer.Server.Infrastructure.Services.Domain.Interfaces";
            var iServicePath =
                @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Infrastructure\Services\Domain\Interfaces";
            var iServiceNamespaces = $"using {dtoNamespace};{Environment.NewLine}" +
                                    $"using {iPageQueryNamesapce};";
            var ignoredServiceNamespaces = new List<string>
            {
                dtoNamespace
            };

            var dtoCodeGenerator = new DtosCodeGenerator(dtoNamespace, dtoPath, modelsNamespace, assembly, false);
            dtoCodeGenerator.Generate();

            var interfaceGenerator =
                new InterfacesCodeGenerator(modelsPath, modelsNamespace, assembly, true, 3, "IGymmerObject", postfix: "Provider");
            interfaceGenerator.Generate();

            var dtoInterfaceGenerator =
                new InterfacesCodeGenerator(dtoPath, dtoNamespace, dtoAssembly, true, 3, "IDto", "Dto");
            dtoInterfaceGenerator.Generate();

            var sortFuncGenerator = new SortFuncCodeGenerator(sortFuncPath, sortFuncNamespace, true);
            sortFuncGenerator.Generate();

            var interfaceRepositoryCodeGenerator = new InterfacesRepositoriesCodeGenerator(modelsNamespace,
                repositoryNamespace, repostioryPath, true, assembly, interfaceRepositoryNamespaces);
            interfaceRepositoryCodeGenerator.Generate();

            var efRepositoryCodeGenerator = new EFRepositoriesCodeGenerator(context, entityMarker, idProvider, modelsNamespace, entityCommonRepositoryNamespace, efRepositoryPath, true, assembly, efRepoNamespaces);
            efRepositoryCodeGenerator.Generate();

            var mapperCodeGenerator = new AutoMapperConfigCodeGenerator(mapperNamespaces,  modelsNamespace,mapperPath, mapperMamespace, true, assembly);
            mapperCodeGenerator.Generate();

            var iPageQueryCodeGenerator = new IPageQueryCodeGenerator(ipageQueryPath, iPageQueryNamesapce, true);
            iPageQueryCodeGenerator.Generate();

            var pageQuery = new PageQueryCodeGenerator(pageQueryPath, pageQueryNamesapce, pageQueryNamespaces, true);
            pageQuery.Generate();

            var serviceCodeGenerator = new InterfacesServicesCodeGenerator(dtoAssembly, assembly, iServiceNamespaces,
                modelsNamespace, iServiceNamespace, iServicePath, dtoNamespace, ignoredServiceNamespaces, true);
            serviceCodeGenerator.Generate();

            var libraryWrappers = Path.Combine(CommonCodePath, "Wrappers");
            var commonWwrappersNamespace = $"{CommonNamespace}.Wrappers";

            var pathNamesapces = $"using {dtoNamespace}.Interfaces;";

            var patchResultCodeGenerator = new PatchResultCodeGenerator(libraryWrappers, commonWwrappersNamespace, true, pathNamesapces);
            patchResultCodeGenerator.Generate();

            var infrastrucutreHelpersPath = Path.Combine(infrastracturePath, "Helpers");
            var infrastructureHelpersNamespace = $"{infrastrucutreNamespace}.Helpers";
            var usingNamespaces = $"using {sortFuncNamespace};{Environment.NewLine}" +
                                  $"using {iPageQueryNamesapce};{Environment.NewLine}" +
                                  $"using {commonExtensionsNamespace};";

            var sortFuncHelperCodeGenerator = new SortFunctionHelperCodeGenerator(
                infrastrucutreHelpersPath, infrastructureHelpersNamespace, usingNamespaces, true);
            sortFuncHelperCodeGenerator.Generate();

            var predicateHelperCodeGenerator =
                new PredicateHelperCodeGenerator(infrastrucutreHelpersPath, infrastructureHelpersNamespace, true);
            predicateHelperCodeGenerator.Generate();

            var stringExtensionCodeGenerator =
                new StringExtensionCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, true);
            stringExtensionCodeGenerator.Generate();

            Console.WriteLine("End.");

            Console.ReadLine();

        }
    }
}
