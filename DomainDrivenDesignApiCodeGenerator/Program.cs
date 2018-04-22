using DomainDrivenDesignApiCodeGenerator.Commands;
using DomainDrivenDesignApiCodeGenerator.Controllers;
using DomainDrivenDesignApiCodeGenerator.Dtos;
using DomainDrivenDesignApiCodeGenerator.Interfaces;
using DomainDrivenDesignApiCodeGenerator.IoC;
using DomainDrivenDesignApiCodeGenerator.Others;
using DomainDrivenDesignApiCodeGenerator.Repositories;
using DomainDrivenDesignApiCodeGenerator.Services;
using DomainDrivenDesignApiCodeGenerator.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;

namespace DomainDrivenDesignApiCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainCodePath = @"D:\Codes\My\Gymmer\src";
            var solutionPath = Path.Combine(mainCodePath, "Gymmer.sln");

            var projectName = "Gymmer";
            var update = false;

            var builder = new Builder(solutionPath);

            var assembly = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\bin\Debug\netcoreapp2.0\Gymmer.Server.Core.dll";
            var commonAssembly = @"D:\Codes\My\Gymmer\src\Gymmer.Common\bin\Debug\netstandard2.0\Gymmer.Common.dll";

            var commonNamespace = $"{projectName}.Common";
            var commonCodePath = Path.Combine(mainCodePath, commonNamespace);
            var commonExtensionsPath = Path.Combine(commonCodePath, "Extensions");
            var commonExtensionsNamespace = $"{commonNamespace}.Extensions";

            var serverSolutionPath = Path.Combine(mainCodePath, $"{projectName}.Server");

            var coreNamespace = $"{projectName}.Server.Core";
            var corePath = Path.Combine(serverSolutionPath, coreNamespace);

            var infrastrucutreNamespace = $"{projectName}.Server.Infrastructure";
            var infrastracturePath = Path.Combine(mainCodePath, $"{projectName}.Server",
                infrastrucutreNamespace);
            var infrastrucutreExtensionsNamespace = $"{infrastrucutreNamespace}.Extensions";
            var infrastractureExtensionsPath = Path.Combine(infrastracturePath, "Extensions");
            var infrastrucutreSettingsNamespace = $"{infrastrucutreNamespace}.Settings";
            var infrastrucutreSettingsPath = Path.Combine(infrastracturePath, "Settings");

            var apiNamespace = $"{projectName}.Server.Api";
            var apiPath = Path.Combine(mainCodePath, $"{projectName}.Server", apiNamespace);

            var apiModelsPath = Path.Combine(apiPath, "Models");


            var coreCommonNamespace = $"{coreNamespace}.Common";

            var dtoNamespace = $"{commonNamespace}.Dtos";
            var modelsNamespace = $"{coreNamespace}.Models";
            var dtoPath = Path.Combine(commonCodePath, "Dtos");
            var modelsPath = Path.Combine(corePath, "Models"); 

            var coreCommonPath = Path.Combine(corePath, "Common");

            var repositoryNamespace = $"{coreNamespace}.Repositories";
            var repostioryPath = Path.Combine(corePath, "Repositories");
            var interfaceRepositoryNamespaces = $"using {coreCommonNamespace}; {Environment.NewLine}" +
                                                $"using {modelsNamespace};";


            var commonExceptionPath = Path.Combine(commonCodePath, "Exceptions");
            var commonExceptionNamespace = $"{commonNamespace}.Exceptions";

            var entityCommonRepositoryNamespace = $"{infrastrucutreNamespace}.Repositories.EF";
            var efRepositoryPath = Path.Combine(infrastracturePath, "Repositories", "EF");
            var efContext = "GymmerContext";
            var entityMarker = "IGymmerObject";
            var idProvider = "IIdProvider";
            var efRepoNamespaces = $"using {coreCommonNamespace}; {Environment.NewLine}" +
                                   $"using {modelsNamespace}; {Environment.NewLine}" +
                                   $"using {modelsNamespace}.Interfaces; {Environment.NewLine}" +
                                   $"using {repositoryNamespace}; {Environment.NewLine}" +
                                   $"using {infrastrucutreNamespace}.Sql;";

            var sqlNamespace = $"{infrastrucutreNamespace}.Sql";

            var mapperPath = Path.Combine(infrastracturePath, "Mappers", "AutoMapperConfig");
            var mapperMamespace = $"{infrastrucutreNamespace}.Mappers";
            var mapperNamespaces = $"using {modelsNamespace}; {Environment.NewLine}" +
                                   $"using {dtoNamespace}; {Environment.NewLine}";

            var ipageQueryPath = Path.Combine(infrastracturePath, "Models", "Interfaces", "IPageQuery");
            var iPageQueryNamesapce = $"{infrastrucutreNamespace}.Models.Interface";

            var pageQueryPath = Path.Combine(apiModelsPath, "PageQuery");
            var pageQueryNamesapce = $"{apiNamespace}.Models";
            var pageQueryNamespaces = $"using {iPageQueryNamesapce};";

            var iServiceNamespace = $"{infrastrucutreNamespace}.Services.Domain.Interfaces";
            var iServicePath = Path.Combine(infrastracturePath, "Services", "Domain", "Interfaces");
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

           // builder.BuildSolution();

            var interfaceGenerator =
                new InterfacesCodeGenerator(modelsPath, modelsNamespace, assembly, update, 3, "IGymmerObject", postfix: "Provider");
            interfaceGenerator.Generate();

            var dtoInterfaceGenerator =
                new InterfacesCodeGenerator(dtoPath, dtoNamespace, commonAssembly, update, 3, "IDto", "Dto", postfix: "Provider");
            dtoInterfaceGenerator.Generate();

            var sortFuncGenerator = new SortFuncCodeGenerator(coreCommonPath, coreCommonNamespace, update);
            sortFuncGenerator.Generate();

            var interfaceRepositoryCodeGenerator = new InterfacesRepositoriesCodeGenerator(modelsNamespace,
                repositoryNamespace, repostioryPath, update, assembly, interfaceRepositoryNamespaces);
            interfaceRepositoryCodeGenerator.Generate();

            var efRepositoryCodeGenerator = new EFRepositoriesCodeGenerator(efContext, entityMarker, idProvider, modelsNamespace, entityCommonRepositoryNamespace, efRepositoryPath, update, assembly, efRepoNamespaces);
            efRepositoryCodeGenerator.Generate();

            var mapperCodeGenerator = new AutoMapperConfigCodeGenerator(mapperNamespaces,  modelsNamespace,mapperPath, mapperMamespace, update, assembly);
            mapperCodeGenerator.Generate();

            var iPageQueryCodeGenerator = new IPageQueryCodeGenerator(ipageQueryPath, iPageQueryNamesapce, update);
            iPageQueryCodeGenerator.Generate();

            var pageQuery = new PageQueryCodeGenerator(pageQueryPath, pageQueryNamesapce, pageQueryNamespaces, update);
            pageQuery.Generate();

            var serviceCodeGenerator = new InterfacesServicesCodeGenerator(commonAssembly, assembly, iServiceNamespaces,
                modelsNamespace, iServiceNamespace, iServicePath, dtoNamespace, ignoredServiceNamespaces, update);
            serviceCodeGenerator.AddIgnoredProps(ignoredProps);
            serviceCodeGenerator.Generate();

            var libraryWrappers = Path.Combine(commonCodePath, "Wrappers");
            var commonWwrappersNamespace = $"{commonNamespace}.Wrappers";

            var pathNamesapces = $"using {dtoNamespace}.Interfaces;";

            var patchResultCodeGenerator = new PatchResultCodeGenerator(libraryWrappers, commonWwrappersNamespace, update, pathNamesapces);
            patchResultCodeGenerator.Generate();

            var infrastrucutreHelpersPath = Path.Combine(infrastracturePath, "Helpers");
            var infrastructureHelpersNamespace = $"{infrastrucutreNamespace}.Helpers";
            var usingNamespaces = $"using {coreCommonNamespace};{Environment.NewLine}" +
                                  $"using {iPageQueryNamesapce};{Environment.NewLine}" +
                                  $"using {commonExtensionsNamespace};";

            var sortFuncHelperCodeGenerator = new SortFunctionHelperCodeGenerator(
                infrastrucutreHelpersPath, infrastructureHelpersNamespace, usingNamespaces, update);
            sortFuncHelperCodeGenerator.Generate();

            var predicateHelperCodeGenerator =
                new PredicateHelperCodeGenerator(infrastrucutreHelpersPath, infrastructureHelpersNamespace, update);
            predicateHelperCodeGenerator.Generate();

            var generalSettingsCodesGenerator =
                new GeneralSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, update);
            generalSettingsCodesGenerator.Generate();

            var stringExtensionCodeGenerator =
                new StringExtensionCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, update);
            stringExtensionCodeGenerator.Generate();

            var extenionsNamespaces = $"using {dtoNamespace};{Environment.NewLine}" +
                                          $"using {dtoNamespace}.Interfaces;{Environment.NewLine}" +
                                          $"using {commonWwrappersNamespace};{Environment.NewLine}" +
                                          $"using {infrastrucutreSettingsNamespace};";

            var cacheExtensionsCodeGenerator = 
                new CacheExtensionCodeGenerator(infrastractureExtensionsPath, infrastrucutreExtensionsNamespace, extenionsNamespaces, update);
            cacheExtensionsCodeGenerator.Generate();

            var jwtDtoNamespaces = $"using {dtoNamespace}.Interfaces;";
            var jwtDtoCodeGenerator = 
                new JwtDtoCodeGenerator(dtoPath, dtoNamespace, jwtDtoNamespaces, update );
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

            var objExtensionCodeGenerator = new ObjectExtensionsCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, update);
            objExtensionCodeGenerator.Generate();

            var commonCommandsNamespace = $"{commonNamespace}.Commands";
            var commandsCodePath = Path.Combine(commonCodePath, "Commands");
            var commandsNamespaces = "";

            var commandCodeGenerator = new CommandsCodeGenerator(commonAssembly, assembly, commandsNamespaces, modelsNamespace, commonCommandsNamespace, commandsCodePath, dtoNamespace, ignoredServiceNamespaces, update);
            commandCodeGenerator.AddIgnoredProps(ignoredProps);
            commandCodeGenerator.Generate();

            var infraCommandsNamespace = $"{infrastrucutreNamespace}.Commands";
            var infraCommandsPatch = Path.Combine(infrastracturePath, "Commands");

            var infraCommandsHandlersNamespace = $"{infraCommandsNamespace}.Handlers";
            var commandsHandlerPatch = Path.Combine(infraCommandsPatch, "Handlers");
            var commandsHandlerNamespaces = $"using {domainServiceNamespace}.Interfaces;";

            var commandsHandlersGenerator = new CommandsHandlersCodeGenerator(commonCommandsNamespace, infraCommandsHandlersNamespace, commandsHandlerPatch, update,
                commonAssembly, commandsHandlerNamespaces, modelsNamespace, assembly);
            commandsHandlersGenerator.Generate();

            var iCommandDispatcherNamespaces = $"using {commonCommandsNamespace};{Environment.NewLine}";

            var iCommandDispatcher = new ICommandDispatcherCodeGenerator(infraCommandsPatch, infraCommandsNamespace, iCommandDispatcherNamespaces, update);
            iCommandDispatcher.Generate();

            var commandDisatcherNamespaces = $"using {commonCommandsNamespace};{Environment.NewLine}" +
                                             $"using {infraCommandsHandlersNamespace};";

            var cmmandDispatcher = new CommandDispatcherCodeGenerator(infraCommandsPatch, infraCommandsNamespace, commandDisatcherNamespaces, update);
            cmmandDispatcher.Generate();

            var infraServicesNamespace = $"{infrastrucutreNamespace}.Services";
            var infraServicesPath = Path.Combine(infrastracturePath, "Services");

            var infraInterfacesServicesNamespace = $"{infraServicesNamespace}.Interfaces";
            var infraInterfacesServicesPath = Path.Combine(infraServicesPath, "Interfaces");
            

            var iJwtNamespaces = $"using {dtoNamespace};";
            var jwtNamespaces =
                $"using {dtoNamespace};{Environment.NewLine}" +
                $"using {commonExceptionNamespace};{Environment.NewLine}" +
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

            var infraServiceDataGeneratorPath = Path.Combine(infraServicesPath, "DataGenerators");
            var infraServiceDataGeneratorNamespace = $"{infraServicesNamespace}.DataGenerators";

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
                                           $"using {infraServiceDataGeneratorNamespace};{Environment.NewLine}" +
                                           $"using {infraSqlNamespace};";

            var settingModuleNamespaces = $"using {infrastrucutreSettingsNamespace};{Environment.NewLine}" +
                                          $"using {infrastrucutreExtensionsNamespace};{Environment.NewLine}";

            var commonSerializersPath = Path.Combine(commonCodePath, "Serializers");
            var commonSerializersNamespace = $"{commonNamespace}.Serializers";

            var apiControllersNamepace = $"{apiNamespace}.Controllers";
            var apiControllersPath = Path.Combine(apiPath, "Controllers");
            var apiControllersNamepaces = $"using {pageQueryNamesapce};{Environment.NewLine}" +
                                          $"using {commonCommandsNamespace};{Environment.NewLine}" +
                                          $"using {dtoNamespace};{Environment.NewLine}" +
                                          $"using {domainServiceNamespace}.Interfaces;{Environment.NewLine}" +
                                          $"using {infraCommandsNamespace};{Environment.NewLine}" +
                                          $"using {infrastrucutreExtensionsNamespace};";

            var apiExceptionMiddlewarePath = Path.Combine(apiPath, "Framework");
            var apiExceptionMiddlewareNamespace = $"{apiNamespace}.Framework";
            var apiExcpetionMiddlewareNamespaces = $"using {commonExceptionNamespace};{Environment.NewLine}" +
                                                   $"using {commonSerializersNamespace};{Environment.NewLine}";

            var apiStartupNamespaces = $"using {apiExceptionMiddlewareNamespace};{Environment.NewLine}" +
                                       $"using {commonSerializersNamespace};{Environment.NewLine}" +
                                       $"using {infraIoCModulesNamespace};{Environment.NewLine}" +
                                       $"using {sqlNamespace};{Environment.NewLine}" +
                                       $"using {infraServiceDataGeneratorNamespace};{Environment.NewLine}" +
                                       $"using {infrastrucutreSettingsNamespace};{Environment.NewLine}";

            new DateTimeExtensionCodeGenerator(commonExtensionsPath, commonExtensionsNamespace, update).Generate();
            new IJwtHandlerCodeGenerator(infraInterfacesServicesPath, infraInterfacesServicesNamespace, iJwtNamespaces, update ).Generate();
            new JwtHandlerCodeGenerator(infraServicesPath, infraServicesNamespace, jwtNamespaces, projectName, update).Generate();
            new JwtSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, update).Generate();
            new IEncrypterCodeGenerator(infraInterfacesServicesPath, infraInterfacesServicesNamespace, update).Generate();
            new EncrypterCodeGenerator(infraServicesPath, infraServicesNamespace, encrypterNamespaces, update).Generate();
            new EmailSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, update).Generate();
            new MailerCodeGenerator(infraServicesPath, infraServicesNamespace, mailerNamespaces, update).Generate();
            new IMailerCodeGenerator(infraInterfacesServicesPath, infraInterfacesServicesNamespace, imailerNamespaces, update).Generate();
            new DataInitalizerCodeGenerator(infraServiceDataGeneratorPath, infraServiceDataGeneratorNamespace, update).Generate();
            new IDataInitalizerCodeGenerator(infraServiceDataGeneratorPath, infraServiceDataGeneratorNamespace, update).Generate();

            new CommandModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, commandsModuleNamespaces, update).Generate();
            new ContainerModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, containerModuleNamespaces, update).Generate();
            new RepositoryModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, repositoryModuleNamespaces, update).Generate();
            new SettingModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, settingModuleNamespaces, update).Generate();
            new ServiceModuleCodeGenerator(infraIoCModulesPath, infraIoCModulesNamespace, servicesModuleNamespaces, efContext, update).Generate();
            new SqlSettingsCodeGenerator(infrastrucutreSettingsPath, infrastrucutreSettingsNamespace, update).Generate();
            new SettingsExtensionCodeGenerator(infrastractureExtensionsPath, infrastrucutreExtensionsNamespace, update).Generate();
            new CustomJsonSerializerCodeGenerator(commonSerializersPath, commonSerializersNamespace, projectName, update).Generate();

            new ControllersCodeGenerator(modelsNamespace, apiControllersNamepace, apiControllersPath, update, assembly, apiControllersNamepaces, commonCommandsNamespace).Generate();
            new StartupCodeGenerator(apiPath, apiNamespace, efContext, apiStartupNamespaces, projectName, update).Generate();
            new CustomExceptionCodeGenerator(commonExceptionPath, commonExceptionNamespace, projectName, update).Generate();
            new ExceptionHandlerMiddlewareCodeGenerator(apiExceptionMiddlewarePath, apiExceptionMiddlewareNamespace, projectName, apiExcpetionMiddlewareNamespaces, update).Generate();
            


            Console.WriteLine("End.");
            Console.ReadLine();

        }
    }
}
