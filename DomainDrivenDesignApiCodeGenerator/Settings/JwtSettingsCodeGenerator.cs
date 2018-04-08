using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator.Settings
{
    public class JwtSettingsCodeGenerator : BaseClassCodeGenerator
    {
        public JwtSettingsCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "JwtSettings"), @namespace, Path.Combine("Settings", "Templates", "JwtSettingsTemplate.txt"), update)
        {
        }
    }
}
