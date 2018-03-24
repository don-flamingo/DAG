using System;
using DomainDrivenDesignApiCodeGenerator.Dtos;
using DomainDrivenDesignApiCodeGenerator.Interfaces;
using DomainDrivenDesignApiCodeGenerator.Others;

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

            var dtoCodeGenerator = new DtoCodeGenerator(dtoNamespace, dtoPath, modelsNamespace, assembly, true);
            dtoCodeGenerator.Generate();

            var interfaceGenerator = new InterfaceCodeGenerator(modelsPath, modelsNamespace, assembly, true, 3, "IGymmerObject");
            interfaceGenerator.Generate();

            var dtoInterfaceGenerator = new InterfaceCodeGenerator(dtoPath, dtoNamespace, dtoAssembly, true, 3, "IDto", "Dto");
            dtoInterfaceGenerator.Generate();

            var sortFuncGenerator = new SortFuncCodeGenerator(sortFuncPath, sortFuncNamespace, true);
            sortFuncGenerator.Generate();

            Console.WriteLine("End.");

            Console.ReadLine();

        }
    }
}
