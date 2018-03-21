using System;
using DomainDrivenDesignApiCodeGenerator.Dtos;

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
            var assembly =
                @"D:\Codes\My\Gymmer\src\Gymmer.Server\Gymmer.Server.Core\bin\Debug\netcoreapp2.0\Gymmer.Server.Core.dll";

            var dtoCodeGenerator = new DtoCodeGenerator(dtoNamespace, dtoPath, modelsNamespace, assembly, isUpdate);
            dtoCodeGenerator.Generate();

            Console.ReadLine();

        }
    }
}
