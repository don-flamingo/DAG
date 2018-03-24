using System;
using DomainDrivenDesignApiCodeGenerator.Dtos;
using DomainDrivenDesignApiCodeGenerator.Interfaces;

namespace DomainDrivenDesignApiCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var isUpdate = true;
            var dtoNamespace = "Gymmer.Framework.Dtos";
            var modelsNamespace = "Gymmer.Server.Core.Models";
            var dtoPath = @"D:\Codes\My\Gymmer\src\Gymmer.Framework\Dtos";
            var modelsPath = @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\Models";
            var assembly =
                @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\bin\Debug\netcoreapp2.0\Gymmer.Server.Core.dll";

            //var dtoCodeGenerator = new DtoCodeGenerator(dtoNamespace, dtoPath, modelsNamespace, assembly, isUpdate);
            //dtoCodeGenerator.Generate();

            var interfaceGenerator = new InterfaceCodeGenerator(modelsPath, modelsNamespace, assembly, true, 3);
            interfaceGenerator.Generate();

            Console.ReadLine();

        }
    }
}
