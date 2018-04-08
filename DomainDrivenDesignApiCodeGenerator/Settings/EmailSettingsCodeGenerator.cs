using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Settings
{
    public class EmailSettingsCodeGenerator : BaseClassCodeGenerator
    {
        public EmailSettingsCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "EmailSettings"), @namespace, Path.Combine("Settings", "Templates", "EmailSettingsTemplate.txt"), update)
        {
        }
    }
}
