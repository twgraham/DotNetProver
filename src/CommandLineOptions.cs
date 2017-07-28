using System;
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace DotNetProver
{
    internal class CommandLineOptions
    {
        public IList<string> Projects { get; private set; }
        public bool VersionsOnly { get; private set; }
        public bool IsHelp { get; private set; }
        public IList<string> RemainingArguments { get; private set; }
        
        public static CommandLineOptions Parse(string[] args)
        {
            var app = new CommandLineApplication(false)
            {
                Name = "dotnet prover",
                FullName = "DotNet Project Version",
                Out = Console.Out,
                Error = Console.Error,
                ExtendedHelpText = @""
            };

            app.HelpOption("-?|-h|--help");

            var optProjects = app.Option("-p|--projects <PROJECTS>", "Project(s) to get version(s) for", CommandOptionType.MultipleValue);
            var versionsOnly = app.Option("-v|--versions-only", "Only print the version(s) for projects",
                CommandOptionType.NoValue);

            if (app.Execute(args) != 0)
                return null;
            
            return new CommandLineOptions()
            {
                Projects = optProjects.Values,
                VersionsOnly = versionsOnly.HasValue(),
                IsHelp = app.IsShowingInformation,
                RemainingArguments = app.RemainingArguments
            };
        }
    }
}