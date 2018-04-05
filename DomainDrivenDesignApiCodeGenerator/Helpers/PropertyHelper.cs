using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DomainDrivenDesignApiCodeGenerator.Interfaces;

namespace DomainDrivenDesignApiCodeGenerator.Helpers
{
    public static class PropertyHelper
    {
        public static string GetPropertyTypeName(this Property property)
        {
            var propertyType = property.Type.Name;

            if (property.Type.IsGenericType)
                propertyType =
                    $"{propertyType}<{property.Type.GetGenericArguments()[0]}>";

            return propertyType.Replace("`1", "");
        }

        public static string GetPropertyTypeName(this PropertyInfo property)
        {
            var propertyType = property.PropertyType.Name;

            if (property.PropertyType.IsGenericType)
                propertyType =
                    $"{propertyType}<{property.PropertyType.GetGenericArguments()[0]}>";

            return propertyType.Replace("`1", "");
        }

        public static string GetPropertyTypeNameForDto(this PropertyInfo property, string modelsNamespace)
        {
            var propertyType = property.PropertyType.Name;

            if (property.PropertyType.IsGenericType)
                propertyType =
                    $"{propertyType}<{ChangePropertyNameToDtoIfIsModel(property.PropertyType.GetGenericArguments()[0], modelsNamespace)}>";
            else
                propertyType = ChangePropertyNameToDtoIfIsModel(property.PropertyType, modelsNamespace);

            return propertyType.Replace("`1", "");
        }

        public static string ChangePropertyNameToDtoIfIsModel(this Type propertyType, string modelsNamespace)
        {
            if (propertyType.Namespace == modelsNamespace)
                return $"{propertyType.Name}Dto";

            return propertyType.Name;
        }
    }
}
