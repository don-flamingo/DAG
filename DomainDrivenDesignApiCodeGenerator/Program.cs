using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Threading;
using DomainDrivenDesignApiCodeGenerator.Commands;
using DomainDrivenDesignApiCodeGenerator.Controllers;
using DomainDrivenDesignApiCodeGenerator.Dtos;
using DomainDrivenDesignApiCodeGenerator.Interfaces;
using DomainDrivenDesignApiCodeGenerator.IoC;
using DomainDrivenDesignApiCodeGenerator.Others;
using DomainDrivenDesignApiCodeGenerator.Repositories;
using DomainDrivenDesignApiCodeGenerator.Services;
using DomainDrivenDesignApiCodeGenerator.Settings;

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
            var infrastrucutreExtensionsNamespace = $"{infrastrucutreNamespace}.Extensions";
            var infrastractureExtensionsPath = Path.Combine(infrastracturePath, "Extensions");
            var infrastrucutreSettingsNamespace = $"{infrastrucutreNamespace}.Settings";
            var infrastrucutreSettingsPath = Path.Combine(infrastracturePath, "Settings");

            var apiNamespace = $"{projectName}.Server.Api";
            var apiPath = Path.Combine(mainCodePath, $"{projectName}.Server", apiNamespace);

            var dtoNamespace = "Gymmer.Common.Dtos";
            var modelsNamespace = "Gymmer.Server.Core.Models";
            var dtoPath = @"D:\Codes\My\Gymmer\src\Gymmer.Common\Dtos";
            var modelsPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Models";
            var assembly =
                @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\bin\Debug\netcoreapp2.0\Gymmer.Server.Core.dll";
            var commonAssembly = @"D:\Codes\My\Gymmer\src\Gymmer.Common\bin\Debug\netstandard2.0\Gymmer.Common.dll";

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
                                   $"using Gymmer.Server.Core.Repositories; {Environment.NewLine}" +
                                   $"using {infrastrucutreNamespace}.Sql;";

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

            var ignoredProps = new List<string>
            {
                "Modified",
                "Status",
                "Created"
            };

            var dtoCodeGenerator = new DtosCodeGenerator(dtoNamespace, dtoPath, modelsNamespace, assembly, false);
            dtoCodeGenerator.Generate();

            var interfaceGenerator =
                new InterfacesCodeGenerator(modelsPath, modelsNamespace, assembly, true, 3, "IGymmerObject", postfix: "Provider");
            interfaceGenerator.Generate();

            var dtoInterfaceGenerator =
                new InterfacesCodeGenerator(dtoPath, dtoNamespace, commonAssembly, true, 3, "IDto", "Dto", postfix: "Provider");
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

            var serviceCodeGenerator = new InterfacesServicesCodeGenerator(commonAssembly, assembly, iServiceNamespaces,
                modelsNamespace, iServiceNamespace, iServicePath, dtoNamespace, ignoredServiceNamespaces, true);
            serviceCodeGenerator.AddIgnoredProps(ignoredProps);
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

            var generalSettingsCodesGenerator =
                new GeneralSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, true);
            generalSettingsCodesGenerator.Generate();

            var stringExtensionCodeGenerator =
                new StringExtensionCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, true);
            stringExtensionCodeGenerator.Generate();

            var extenionsNamespaces = $"using {dtoNamespace};{Environment.NewLine}" +
                                          $"using {dtoNamespace}.Interfaces;{Environment.NewLine}" +
                                          $"using {commonWwrappersNamespace};{Environment.NewLine}" +
                                          $"using {infrastrucutreSettingsNamespace};";

            var cacheExtensionsCodeGenerator = 
                new CacheExtensionCodeGenerator(infrastractureExtensionsPath, infrastrucutreExtensionsNamespace, extenionsNamespaces, true);
            cacheExtensionsCodeGenerator.Generate();

            var jwtDtoNamespaces = $"using {dtoNamespace}.Interfaces;";
            var jwtDtoCodeGenerator = 
                new JwtDtoCodeGenerator(dtoPath, dtoNamespace, jwtDtoNamespaces, true );
            jwtDtoCodeGenerator.Generate();

            var domainServicesPath = Path.Combine(infrastracturePath , "Services", "Domain");
            var domainServiceNamespace = $"{infrastrucutreNamespace}.Services.Domain";
            var domainServicesNamespaces = $"using {dtoNamespace};{Environment.NewLine}" +
                                           $"using {commonExtensionsNamespace};{Environment.NewLine}" +
                                           $"using {commonWwrappersNamespace};{Environment.NewLine}" +
                                           $"using {repositoryNamespace};{Environment.NewLine}" +
                                           $"using {modelsNamespace};{Environment.NewLine}" +
                                           $"using {iServiceNamespace};{Environment.NewLine}" +
                                           $"using {infrastructureHelpersNamespace};{Environment.NewLine}" +
                                           $"using {iPageQueryNamesapce};{Environment.NewLine}" +
                                           $"using {infrastrucutreExtensionsNamespace};{Environment.NewLine}";

            var domainServicesCodeGenerator = new DomainServicesCodeGenerator(commonAssembly, assembly,
                domainServicesNamespaces, modelsNamespace, domainServiceNamespace,
                domainServicesPath, dtoNamespace, ignoredServiceNamespaces, false);
            domainServicesCodeGenerator.AddIgnoredProps(ignoredProps);
            domainServicesCodeGenerator.Generate();

            var objExtensionCodeGenerator = new ObjectExtensionsCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, true);
            objExtensionCodeGenerator.Generate();

            var commonCommandsNamespace = $"{CommonNamespace}.Commands";
            var commandsCodePath = Path.Combine(CommonCodePath, "Commands");
            var commandsNamespaces = "";

            var commandCodeGenerator = new CommandsCodeGenerator(commonAssembly, assembly, commandsNamespaces, modelsNamespace, commonCommandsNamespace, commandsCodePath, dtoNamespace, ignoredServiceNamespaces, true);
            commandCodeGenerator.AddIgnoredProps(ignoredProps);
            commandCodeGenerator.Generate();

            var infraCommandsNamespace = $"{infrastrucutreNamespace}.Commands";
            var infraCommandsPatch = Path.Combine(infrastracturePath, "Commands");

            var infraCommandsHandlersNamespace = $"{infraCommandsNamespace}.Handlers";
            var commandsHandlerPatch = Path.Combine(infraCommandsPatch, "Handlers");
            var commandsHandlerNamespaces = $"using {domainServiceNamespace}.Interfaces;";

            var commandsHandlersGenerator = new CommandsHandlersCodeGenerator(commonCommandsNamespace, infraCommandsHandlersNamespace, commandsHandlerPatch, true,
                commonAssembly, commandsHandlerNamespaces, modelsNamespace, assembly);
            commandsHandlersGenerator.Generate();

            var iCommandDispatcherNamespaces = $"using {commonCommandsNamespace};{Environment.NewLine}";

            var iCommandDispatcher = new ICommandDispatcherCodeGenerator(infraCommandsPatch, infraCommandsNamespace, iCommandDispatcherNamespaces, true);
            iCommandDispatcher.Generate();

            var commandDisatcherNamespaces = $"using {commonCommandsNamespace};{Environment.NewLine}" +
                                             $"using {infraCommandsHandlersNamespace};";

            var cmmandDispatcher = new CommandDispatcherCodeGenerator(infraCommandsPatch, infraCommandsNamespace, commandDisatcherNamespaces, true);
            cmmandDispatcher.Generate();

            var infraServicesNamespace = $"{infrastrucutreNamespace}.Services";
            var infraServicesPath = Path.Combine(infrastracturePath, "Services");

            var infraInterfacesServicesNamespace = $"{infraServicesNamespace}.Interfaces";
            var infraInterfacesServicesPath = Path.Combine(infraServicesPath, "Interfaces");
            

            var iJwtNamespaces = $"using {dtoNamespace};";
            var jwtNamespaces =
                $"using {dtoNamespace};{Environment.NewLine}" +
                $"using {commonExtensionsNamespace};{Environment.NewLine}" +
                $"using {infrastrucutreSettingsNamespace};{Environment.NewLine}" +
                $"using {infraInterfacesServicesNamespace};{Environment.NewLine}";
            var encrypterNamespaces =
                $"using {commonExtensionsNamespace};{Environment.NewLine}" +
                $"using {infraInterfacesServicesNamespace};";
            var imailerNamespaces = $"using {modelsNamespace};";
            var mailerNamespaces =
                $"using {modelsNamespace};{Environment.NewLine}" +
                $"using {infraInterfacesServicesNamespace};{Environment.NewLine}" +
                $"using {infrastrucutreSettingsNamespace};{Environment.NewLine}";

            var infraIoCPath = Path.Combine(infrastracturePath, "IoC");
            var infraIoCNamespace = $"{infrastrucutreNamespace}.IoC";
            var infraIoCModulesPath = Path.Combine(infraIoCPath, "Modules");
            var infraIoCModulesNamespace = $"{infraIoCNamespace}.Modules";
            var infraSqlNamespace = $"{infrastrucutreNamespace}.Sql";

            var commandsModuleNamespaces = $"using {commonCommandsNamespace};{Environment.NewLine}" +
                                           $"using {infraCommandsNamespace};{Environment.NewLine}" +
                                           $"using {infraCommandsHandlersNamespace};";

            var containerModuleNamespaces = $"using {infraIoCModulesNamespace};{Environment.NewLine}" +
                                            $"using {mapperMamespace};{Environment.NewLine}" +
                                            $"using {infrastrucutreExtensionsNamespace};{Environment.NewLine}";

            var repositoryModuleNamespaces = $"using {repositoryNamespace};{Environment.NewLine}" +
                                             $"using {entityCommonRepositoryNamespace};{Environment.NewLine}";

            var servicesModuleNamespaces = $"using {infraServicesNamespace};{Environment.NewLine}" +
                                           $"using {infraServicesNamespace}.Interfaces;{Environment.NewLine}" +
                                           $"using {domainServiceNamespace};{Environment.NewLine}" +
                                           $"using {domainServiceNamespace}.Interfaces;{Environment.NewLine}" +
                                           $"using {infraSqlNamespace};";

            var settingModuleNamespaces = $"using {infrastrucutreSettingsNamespace};{Environment.NewLine}" +
                                          $"using {infrastrucutreExtensionsNamespace};{Environment.NewLine}";

            var commonSerializersPath = Path.Combine(CommonCodePath, "Serializers");
            var commonSerializersNamespace = $"{CommonNamespace}.Serializers";

            var apiControllersNamepace = $"{apiNamespace}.Controllers";
            var apiControllersPath = Path.Combine(apiPath, "Controllers");
            var apiControllersNamepaces = $"using {pageQueryNamesapce};{Environment.NewLine}" +
                                          $"using {commonCommandsNamespace};{Environment.NewLine}" +
                                          $"using {dtoNamespace};{Environment.NewLine}" +
                                          $"using {domainServiceNamespace}.Interfaces;{Environment.NewLine}" +
                                          $"using {infraCommandsNamespace};{Environment.NewLine}" +
                                          $"using {infrastrucutreExtensionsNamespace};";
            

            new DateTimeExtensionCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, true).Generate();
            new IJwtHandlerCodeGenerator(infraInterfacesServicesPath, infraInterfacesServicesNamespace, iJwtNamespaces, true ).Generate();
            new JwtHandlerCodeGenerator(infraServicesPath, infraServicesNamespace, jwtNamespaces, true).Generate();
            new JwtSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, true).Generate();
            new IEncrypterCodeGenerator(infraInterfacesServicesPath, infraInterfacesServicesNamespace, true).Generate();
            new EncrypterCodeGenerator(infraServicesPath, infraServicesNamespace, encrypterNamespaces, true).Generate();
            new EmailSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, true).Generate();
            new MailerCodeGenerator(infraServicesPath, infraServicesNamespace, mailerNamespaces, true).Generate();
            new IMailerCodeGenerator(infraInterfacesServicesPath, infraInterfacesServicesNamespace, imailerNamespaces, true).Generate();

            new CommandModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, commandsModuleNamespaces, true).Generate();
            new ContainerModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, containerModuleNamespaces, true).Generate();
            new RepositoryModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, repositoryModuleNamespaces, true).Generate();
            new SettingModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, settingModuleNamespaces, true).Generate();
            new ServiceModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, servicesModuleNamespaces, context, true).Generate();
            new SqlSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, true).Generate();
            new SettingsExtensionCodeGenerator(infrastractureExtensionsPath, infrastrucutreExtensionsNamespace, true).Generate();
            new CustomJsonSerializerCodeGenerator(commonSerializersPath, commonSerializersNamespace, projectName, true).Generate();

            new ControllersCodeGenerator(modelsNamespace, apiControllersNamepace, apiControllersPath, true, assembly, apiControllersNamepaces, commonCommandsNamespace).Generate();

            Console.WriteLine("End.");
            Console.ReadLine();

        }
    }
}
