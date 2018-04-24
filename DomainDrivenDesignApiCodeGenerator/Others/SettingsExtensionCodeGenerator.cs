using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Others
{
    public class SettingsExtensionCodeGenerator : BaseClassCodeGenerator
    {
        public SettingsExtensionCodeGenerator(string dirPath, string @namespace, bool update)
            : base(Path.Combine(dirPath, "SettingsExtension"), @namespace, Path.Combine("Others", "Templates", "SettingsExtensionsTemplate.txt"), update, "")
        {

        }
    }
}
