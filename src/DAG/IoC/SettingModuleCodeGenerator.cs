using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.IoC
{
    public class SettingModuleCodeGenerator : BaseClassCodeGenerator
    {
        public SettingModuleCodeGenerator(string dirPath, string @namespace, string namespaces, bool update)
            : base(Path.Combine(dirPath, "SettingModule"), @namespace, Path.Combine("IoC", "Templates", "SettingModuleTemplate.txt"), update)
        {
            AddBodyTemplateResolver(Consts.Namespaces, namespaces);
        }
    }
}
