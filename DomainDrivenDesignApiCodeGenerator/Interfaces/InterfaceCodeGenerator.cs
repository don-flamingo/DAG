using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using DomainDrivenDesignApiCodeGenerator.Helpers;

namespace DomainDrivenDesignApiCodeGenerator.Interfaces
{
    public class InterfaceCodeGenerator
    {
        private readonly string _modelsPath;
        private readonly string _namespace;
        private readonly string _assemblyPath;
        private readonly string _interfacesNamespace;
        private readonly bool _update;
        private readonly int _minUnionProperty;

        private readonly IList<Property> _properties;
        private readonly IDictionary<Property, string> _interfacesNameForProperties;

        public InterfaceCodeGenerator(string modelsPath, string namespaceS, string assemblyPath, bool update,
            int minUnionProperty)
        {
            _modelsPath = modelsPath;
            _namespace = namespaceS;
            _assemblyPath = assemblyPath;
            _update = update;
            _minUnionProperty = minUnionProperty;
            _interfacesNamespace = $"{_namespace}.Interfaces";

            _properties = new List<Property>();
            _interfacesNameForProperties = new Dictionary<Property, string>();
        }

        public void Generate()
        {
            var assembly = Assembly.LoadFrom(_assemblyPath);
            var models = assembly.GetClassFromAssemblyNamespace(_namespace)
                .ToList();

            FillPropertiesList(models);
            GenerateInterfaces(models);
            GenerateClassesWithInterfaces(models);
        }

        private void FillPropertiesList(IEnumerable<Type> models)
        {
            foreach (var model in models)
            {
                foreach (var propertyInfo in model.GetProperties())
                {
                    var propertFromList = _properties
                        .FirstOrDefault(p => p.Name == propertyInfo.Name && p.Type == propertyInfo.PropertyType);

                    if (propertFromList == null)
                    {
                        if(propertyInfo.PropertyType.Namespace != _namespace)
                            AddNewPropertyToList(propertyInfo);

                        continue;
                    }

                    propertFromList.Count++;
                }
            }
        }

        private void AddNewPropertyToList(PropertyInfo propertyInfo)
        {
            var property = new Property
            {
                Count = 1,
                Name = propertyInfo.Name,
                Type = propertyInfo.PropertyType
            };

            _properties.Add(property);
        }

        private void GenerateInterfaces(IEnumerable<Type> models)
        {
            var propertiesForInterfaces = GetPropertiesForInterfaces();
            var interfacesDirectory = Path.Combine(_modelsPath, "Interfaces");

            if (!Directory.Exists(interfacesDirectory))
                Directory.CreateDirectory(interfacesDirectory);

            foreach (var property in propertiesForInterfaces)
            {
                var needTypeForName =
                    propertiesForInterfaces.Any(p => p.Name == property.Name && p.Type != property.Type);
                var interfaceName = $"I{property.Name}";

                if (needTypeForName)
                    interfaceName = $"{interfaceName}{property.Type.Name}";

                var interfacePath = Path.Combine(interfacesDirectory, $"{interfaceName}.g.cs");
                var interfaceBody = GetInterfaceBody(interfaceName, property);

                _interfacesNameForProperties.Add(property, interfaceName);

                if (File.Exists(interfacePath) && !_update)
                    return;

                File.WriteAllText(interfacePath, interfaceBody);
            }
        }

        private string GetInterfaceBody(string interfaceName, Property property)
        {
            var propertyCode = $"{property.GetPropertyTypeName()} {property.Name} {{ get; set; }}";
            var interfaceTemplate = File.ReadAllText(@"Interfaces\InterfaceTemplate.txt");

            return interfaceTemplate.Replace(Consts.CLASSNAME, interfaceName)
                .Replace(Consts.NAMESPACE, _interfacesNamespace)
                .Replace(Consts.BODY, propertyCode);
        }

        private void GenerateClassesWithInterfaces(IEnumerable<Type> models)
        {
            var propertiesForInterfaces = GetPropertiesForInterfaces();
            var extensionDirectory = Path.Combine(_modelsPath, "Extensions");

            if (!Directory.Exists(extensionDirectory))
                Directory.CreateDirectory(extensionDirectory);

            foreach (var model in models)
            {
                var interfacesStringBuilder = GetInterfaces(propertiesForInterfaces, model);
                var path = Path.Combine(extensionDirectory, $"{model.Name}.g.cs");
                var modelExtensionBody = File.ReadAllText(@"Interfaces\ModelExtensionTemplate.txt")
                    .Replace(Consts.CLASSNAME, model.Name)
                    .Replace(Consts.NAMESPACE, _namespace)
                    .Replace(Consts.INTERFACES, interfacesStringBuilder.ToString());

                if (File.Exists(path) && !_update)
                    continue;

                File.WriteAllText(path, modelExtensionBody);
            }
        }

        private List<Property> GetPropertiesForInterfaces()
            => _properties.Where(x => x.Count >= _minUnionProperty).ToList();

        private StringBuilder GetInterfaces(List<Property> propertiesForInterfaces, Type model)
        {
            var interfacesStringBuilder = new StringBuilder();
            var modelProperties = model.GetProperties().ToList();
            var propertiesForGenerateClass = propertiesForInterfaces
                .Where(property => modelProperties.Exists(modelProperty =>
                    modelProperty.PropertyType == property.Type &&
                    modelProperty.Name == property.Name))
                    .ToList();

            propertiesForGenerateClass.ForEach(pi => interfacesStringBuilder.Append($"{_interfacesNameForProperties[pi]}, "));
            interfacesStringBuilder = interfacesStringBuilder.Remove(interfacesStringBuilder.Length - 2, 2); // remove last ', '
            return interfacesStringBuilder;
        }


    }
}
