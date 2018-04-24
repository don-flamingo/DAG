using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Settings
{
    public class EmailSettingsCodeGenerator : BaseClassCodeGenerator
    {
        public EmailSettingsCodeGenerator(string filePath, string @namespace, bool update) 
            : base(Path.Combine(filePath, "EmailSettings"), @namespace, Path.Combine("Settings", "Templates", "EmailSettingsTemplate.txt"), update)
        {
        }
    }
}
