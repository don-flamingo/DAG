using System;
using System.Diagnostics;
using System.Text;

namespace DAG
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
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();

            var proessInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build {_slnPath}",
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using (var process = System.Diagnostics.Process.Start(proessInfo))
            {
                stdout.AppendLine(process.StandardOutput.ReadToEnd());
                stderr.AppendLine(process.StandardError.ReadToEnd());

                process.WaitForExit();
            }

            Console.WriteLine(stdout);
            Console.WriteLine(stderr);
            Console.WriteLine("Build success");
        }
    }
}
