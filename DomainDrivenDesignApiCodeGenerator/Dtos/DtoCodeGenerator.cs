using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Dtos
{
    public class DtoCodeGenerator : BaseDtoCodeGenerator
    {
        public DtoCodeGenerator(string dtoNamespaceS, string dtosModelsPath, string modelsNamespace,
            string assemblyPath, bool isUpdate) : base(dtoNamespaceS, dtosModelsPath, modelsNamespace, assemblyPath,
            isUpdate)
        {
        }

        public override void Generate()
        {
            var propertyTemplate = File.ReadAllText(@"Dtos\DtoPropertyTemplate.txt");
            var dtoTemplate = File.ReadAllText(@"Dtos\DtoTemplate.txt");
            var assembly = Assembly.LoadFile(_assemblyPath);
            var models = assembly.GetExportedTypes()
                .Where(x => x.Namespace == _modelsNamespace);

            foreach (var model in models)
            {
                var propertyStringBuilder = new StringBuilder();
                var dtoName = $"{model.Name}Dto";
                foreach (var property in model.GetProperties())
                {
                    var propertyType = GetPropertyType(property);
                    var propertyField = $"public {propertyType} {property.Name} {{ get; set; }}";
                    propertyField = propertyTemplate.Replace(Consts.PROPERTY, propertyField);
                    propertyStringBuilder.AppendLine(propertyField);
                }

                var dto = dtoTemplate.Replace(Consts.CLASSNAME, dtoName)
                    .Replace(Consts.NAMESPACE, _dtoNamespace)
                    .Replace(Consts.BODY, propertyStringBuilder.ToString());

                if (!Directory.Exists(_dtosModelsPath))
                    Directory.CreateDirectory(_dtosModelsPath);

                var dtoPath = Path.Combine(_dtosModelsPath, $"{dtoName}.g.cs");

                if ((File.Exists(dtoPath) && _isUpdate) || !File.Exists(dtoPath))
                {
                    File.WriteAllText(dtoPath, dto);
                    Console.WriteLine($"Dto {dtoName} was created");
                }
            }
        }

        private string GetPropertyType(PropertyInfo property)
        {
            var propertyType = property.PropertyType.Name;

            if (property.PropertyType.IsGenericType)
                propertyType =
                    $"{propertyType}<{ChangePropertyNameToDtoIfIsModel(property.PropertyType.GetGenericArguments()[0])}>";
            else 
                propertyType = ChangePropertyNameToDtoIfIsModel(property.PropertyType);

            return propertyType.Replace("`1", "");
        }

        private string ChangePropertyNameToDtoIfIsModel(Type propertyType)
        {
            if (propertyType.Namespace == _modelsNamespace)
                return $"{propertyType.Name}Dto";

            return propertyType.Name;
        }
    }


}
