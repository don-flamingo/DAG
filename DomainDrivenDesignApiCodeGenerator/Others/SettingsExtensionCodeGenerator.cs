using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Others
{
    public class SettingsExtensionCodeGenerator : BaseClassCodeGenerator
    {
        public SettingsExtensionCodeGenerator(string dirPath, string @namespace, bool update)
            : base(Path.Combine(dirPath, "SettingsExtension"), @namespace, Path.Combine("Others", "Templates", "SettingsExtensionsTemplate.txt"), update, "")
        {

        }
    }
}
