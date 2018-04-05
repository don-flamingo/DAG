using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Settings
{
    public class GeneralSettingsCodeGenerator : BaseClassCodeGenerator
    {
        public GeneralSettingsCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "GeneralSettings"), @namespace, Path.Combine("Settings", "Templates", "GeneralSettingsTemplate.txt"), update)
        {
        }
    }
}
