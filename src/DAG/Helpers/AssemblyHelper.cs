using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAG.Helpers
{
    public static class AssemblyHelper
    {
        public static System.Collections.Generic.IEnumerable<Type> GetClassFromAssemblyNamespace(this Assembly assembly, string modelsNamespace)
        {
            var models = assembly.GetExportedTypes()
                .Where(x => x.Namespace == modelsNamespace);
            return models;
        }
    }
}
