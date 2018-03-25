using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Helpers;

namespace DomainDrivenDesignApiCodeGenerator.Dtos
{
    public class DtoCodeGenerator : BaseDtoCodeGenerator
    {
        public DtoCodeGenerator(string dtoNamespaceS, string classDirectoryPath, string modelsNamespace,
            string assemblyPath, bool isUpdate) : base(dtoNamespaceS, classDirectoryPath, modelsNamespace, assemblyPath,
            isUpdate)
        {
        }

        public override void Generate()
        {
            var propertyTemplate = File.ReadAllText(@"Dtos\DtoPropertyTemplate.txt");
            var dtoTemplate = File.ReadAllText(@"Dtos\DtoTemplate.txt");
            System.Collections.Generic.IEnumerable<Type> models = GetModelsFromAssembly(_modelsNamespace);

            foreach (var model in models)
            {
                var propertyStringBuilder = new StringBuilder();
                var dtoName = $"{model.Name}Dto";
                foreach (var property in model.GetProperties())
                {
                    var propertyType = property.GetPropertyTypeNameForDto(_modelsNamespace);
                    var propertyField = $"public {propertyType} {property.Name} {{ get; set; }}";
                    propertyField = propertyTemplate.Replace(Consts.Property, propertyField);
                    propertyStringBuilder.AppendLine(propertyField);
                }

                var dto = dtoTemplate.Replace(Consts.Classname, dtoName)
                    .Replace(Consts.Namespace, _dtoNamespace)
                    .Replace(Consts.Body, propertyStringBuilder.ToString());

                if (!Directory.Exists(_classDirectoryPath))
                    Directory.CreateDirectory(_classDirectoryPath);

                var dtoPath = Path.Combine(_classDirectoryPath, $"{dtoName}.g.cs");

                if ((File.Exists(dtoPath) && _isUpdate) || !File.Exists(dtoPath))
                {
                    File.WriteAllText(dtoPath, dto);
                    Console.WriteLine($"Dto {dtoName} was created");
                }
            }
        }


    }


}
