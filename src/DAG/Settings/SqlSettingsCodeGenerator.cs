using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DAG;

namespace DAG.Settings
{
    public class SqlSettingsCodeGenerator : BaseClassCodeGenerator
    {
        public SqlSettingsCodeGenerator(string filePath, string @namespace, bool update)
            : base(Path.Combine(filePath, "SqlSettings"), @namespace, Path.Combine("Settings", "Templates", "SqlSettingsTemplate.txt"), update)
        {
        }
    }
}

