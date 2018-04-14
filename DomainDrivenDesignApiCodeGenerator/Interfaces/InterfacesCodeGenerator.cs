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
    public class InterfacesCodeGenerator : BaseCodeGenerator
    {
        private readonly string _namespace;
        private readonly string _interfacesNamespace;
        private readonly string _markerInterface;
        private readonly string _prefix;
        private readonly string _postfix;
        private readonly bool _update;
        private readonly int _minUnionProperty;

        private readonly IList<Property> _properties;
        private readonly IDictionary<Property, string> _interfacesNameForProperties;

        public InterfacesCodeGenerator(string classDirectoryPath, string namespaceS, string assemblyPath, bool update,
            int minUnionProperty, string markerInterface, string prefix = "", string postfix = "") : base(assemblyPath, classDirectoryPath)
        {
            _namespace = namespaceS;
            _update = update;
            _minUnionProperty = minUnionProperty;
            _prefix = prefix;
            _postfix = postfix;

            _markerInterface = markerInterface;

            _interfacesNamespace = $"{_namespace}.Interfaces";

            _properties = new List<Property>();
            _interfacesNameForProperties = new Dictionary<Property, string>();
        }

        public override void Generate()
        {
            var models = GetModelsFromAssembly(_namespace).ToList();

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
            var interfacesDirectory = Path.Combine(_classDirectoryPath, "Interfaces");

            if (!Directory.Exists(interfacesDirectory))
                Directory.CreateDirectory(interfacesDirectory);

            CreateMarkerInteface(interfacesDirectory);

            foreach (var property in propertiesForInterfaces)
            {
                var needTypeForName =
                    propertiesForInterfaces.Any(p => p.Name == property.Name && p.Type != property.Type);
                var interfaceName = $"I{_prefix}{property.Name}{_postfix}";

                if (needTypeForName)
                    interfaceName = $"{interfaceName}{property.Type.Name}";

                var interfacePath = Path.Combine(interfacesDirectory, $"{interfaceName}.g.cs");
                var interfaceBody = GetInterfaceBody(interfaceName, property);

                _interfacesNameForProperties.Add(property, interfaceName);

                if (File.Exists(interfacePath) && !_update)
                    return;

                File.WriteAllText(interfacePath, interfaceBody);
                Console.WriteLine($"{interfaceName} created");
            }
        }

        private void CreateMarkerInteface(string interfacesDirectory)
        {
            var interfaceTemplate = File.ReadAllText(@"Interfaces\InterfaceTemplate.txt");
            var interfacePath = Path.Combine(interfacesDirectory, $"{_markerInterface}.g.cs");
            var body = interfaceTemplate.Replace(Consts.ClassName, _markerInterface)
                .Replace(Consts.Namespace, _interfacesNamespace)
                .Replace(Consts.Body, "");

            if (File.Exists(interfacePath) && !_update)
                return;

            File.WriteAllText(interfacePath, body);
            Console.WriteLine($"{_markerInterface} created");
        }

        private string GetInterfaceBody(string interfaceName, Property property)
        {
            var propertyCode = $"{property.GetPropertyTypeName()} {property.Name} {{ get; set; }}";
            var interfaceTemplate = File.ReadAllText(@"Interfaces\InterfaceTemplate.txt");

            return interfaceTemplate.Replace(Consts.ClassName, interfaceName)
                .Replace(Consts.Namespace, _interfacesNamespace)
                .Replace(Consts.Body, propertyCode);
        }

        private void GenerateClassesWithInterfaces(IEnumerable<Type> models)
        {
            var propertiesForInterfaces = GetPropertiesForInterfaces();
            var extensionDirectory = Path.Combine(_classDirectoryPath, "Extensions");

            if (!Directory.Exists(extensionDirectory))
                Directory.CreateDirectory(extensionDirectory);

            foreach (var model in models)
            {
                var interfacesStringBuilder = GetInterfaces(propertiesForInterfaces, model);
                var path = Path.Combine(extensionDirectory, $"{model.Name}.g.cs");
                var modelExtensionBody = File.ReadAllText(@"Interfaces\ModelExtensionTemplate.txt")
                    .Replace(Consts.ClassName, model.Name)
                    .Replace(Consts.Namespace, _namespace)
                    .Replace(Consts.Interfaces, interfacesStringBuilder.ToString());

                if (File.Exists(path) && !_update)
                    continue;

                File.WriteAllText(path, modelExtensionBody);
                Console.WriteLine($"{model.Name} extension created");
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

            interfacesStringBuilder.Append($"{_markerInterface}, ");

            propertiesForGenerateClass.ForEach(pi => interfacesStringBuilder.Append($"{_interfacesNameForProperties[pi]}, "));
            interfacesStringBuilder = interfacesStringBuilder.Remove(interfacesStringBuilder.Length - 2, 2); // remove last ', '
            return interfacesStringBuilder;
        }


    }
}
