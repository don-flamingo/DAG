using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DAG.Process
{
    // https://medium.com/@equiman/run-a-command-in-external-terminal-with-net-core-cc24e3cc9839
    public static class Shell
    {
        public static Response Term(this string cmd, Output? output = Output.Hidden, string dir = "")
        {
            var result = new Response();
            var stderr = new StringBuilder();
            var stdout = new StringBuilder();
            try
            {
                string fnm = "";
                Args(ref fnm, ref cmd, output, dir);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = fnm;
                startInfo.Arguments = cmd;
                startInfo.RedirectStandardOutput = !(output == Output.External);
                startInfo.RedirectStandardError = !(output == Output.External);
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = !(output == Output.External);
                if (!String.IsNullOrEmpty(dir) && output != Output.External)
                {
                    startInfo.WorkingDirectory = dir;
                }

                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo))
                {
                    switch (output)
                    {
                        case Output.Internal:

                            while (!process.StandardOutput.EndOfStream)
                            {
                                string line = process.StandardOutput.ReadLine();
                                stdout.AppendLine(line);
                                Console.WriteLine(line);
                            }

                            while (!process.StandardError.EndOfStream)
                            {
                                string line = process.StandardError.ReadLine();
                                stderr.AppendLine(line);
                                Console.WriteLine(line);
                            }
                            break;
                        case Output.Hidden:
                            stdout.AppendLine(process.StandardOutput.ReadToEnd());
                            stderr.AppendLine(process.StandardError.ReadToEnd());
                            break;
                    }
                    process.WaitForExit();
                    result.Stdout = stdout.ToString();
                    result.Stderr = stderr.ToString();
                    result.Code = process.ExitCode;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return result;
        }

        private static void Args(ref string fnm, ref string cmd, Output? output = Output.Hidden, string dir = "")
        {
            try
            {
                if (!String.IsNullOrEmpty(dir))
                {
                    if (!Directory.Exists(dir))
                        throw new ArgumentException("dir doesn't exist!");
                }

                switch (OS.WhatIs())
                {
                    case "win":
                        fnm = "cmd.exe";
                        if (!String.IsNullOrEmpty(dir))
                        {
                            dir = $" \"{dir}\"";
                        }
                        if (output == Output.External)
                        {
                            cmd = $"{Directory.GetCurrentDirectory()}/cmd.win.bat \"{cmd}\"{dir}";
                        }
                        cmd = $"/c \"{cmd}\"";
                        break;
                    case "mac":
                        fnm = "/bin/bash";
                        if (!String.IsNullOrEmpty(dir))
                        {
                            dir = $" '{dir}'";
                        }
                        if (output == Output.External)
                        {
                            cmd = $"sh {Directory.GetCurrentDirectory()}/cmd.mac.sh '{cmd}'{dir}";
                        }
                        cmd = $"-c \"{cmd}\"";
                        break;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

        }
    }
}
