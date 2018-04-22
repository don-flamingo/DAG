using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DomainDrivenDesignApiCodeGenerator
{
    public class Builder
    {
        private readonly string _slnPath;

        public Builder(string slnPath)
        {
            _slnPath = slnPath;
        }

        public void BuildSolution()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"build {@_slnPath}",
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = false
                }

            };

            process.Start();
            process.WaitForExit();
        }
    }
}
