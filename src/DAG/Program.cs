using DAG.Commands;
using DAG.Controllers;
using DAG.Dtos;
using DAG.Interfaces;
using DAG.IoC;
using DAG.Others;
using DAG.Repositories;
using DAG.Services;
using DAG.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using DAG;
using DAG.Extensions;

namespace DAG
{
    class Program
    {

        private static readonly string _errorParameterMessage =
            "One paramter is invalid, please chceck below paramteres information and try again. " + Environment.NewLine +
            @"   -p projectPath ex: -p C:\Repository\Codes\Api directory must exists and must have construction descripted in readme" + Environment.NewLine +
            "   -n projectName ex: -n Shop (only letters or digits, special characters are not allowed)" + Environment.NewLine +
            "   -u update (true | false) (not required, default value is false)";

        static void Main(string[] args)
        {
            var (projectPath, projectName, update) = GetParametersFromArgs(args);
            MainGenerator.Generate(projectPath, projectName, update);
        }

        private static (string, string, bool) GetParametersFromArgs(IReadOnlyList<string> args)
        {
            const string projectNameTemplate = "-n";
            const string projectPathTemplate = "-p";
            const string updateTemplate = "-u";

            var projectPath = string.Empty;
            var projectName = string.Empty;
            var update = false;
            var throwEx = false;

            try
            {

                for (var i = 0; i < args.Count; i++)
                {
                    switch (args[i])
                    {
                        case projectNameTemplate:
                            projectName = args[i + 1];

                            if (projectName.Contains("."))
                                throwEx = true;

                            break;
                        case projectPathTemplate:
                            projectPath = args[i + 1];

                            if (!Directory.Exists(projectPath))
                                throwEx = true;

                            break;
                        case updateTemplate:
                            update = bool.Parse(args[i + 1]);
                            break;
                    }
                }

                if (throwEx || projectName.Empty() || projectPath.Empty())
                    throw new Exception("invalid_param");

            }
            catch 
            {
                Console.Write(_errorParameterMessage );
                Console.ReadKey();
                Environment.Exit(-1);
            }

            return (projectPath, projectName, update);
        }
    }
}
