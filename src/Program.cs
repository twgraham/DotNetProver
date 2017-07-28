using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DotNetProver.Tools;
using Microsoft.Extensions.CommandLineUtils;

namespace DotNetProver
{
    class Program
    {
        static int Main(string[] args)
        {
            CommandLineOptions options;
            try
            {
                options = CommandLineOptions.Parse(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex);
                return 1;
            }

            if (options == null)
                return 1;

            if (options.IsHelp)
                return 2;

            var projectVersions = GetProjectVersion(options.Projects);

            if (projectVersions == null)
                return 1;
            
            if (options.VersionsOnly)
                projectVersions.Values.ToList().ForEach(Console.WriteLine);
            else
                projectVersions.Select(x => x).ToList().ForEach(x => Console.WriteLine($"{x.Key}:{x.Value}"));

            return 0;
        }

        private static IDictionary<string, string> GetProjectVersion(IList<string> projects)
        {
            var projectVersions = new Dictionary<string, string>();
            try
            {
                foreach (var project in projects)
                {
                    var projectLocation = MsBuildProjectFinder.FindMsBuildProject(Directory.GetCurrentDirectory(), projects.First());
                    var projectXml = XElement.Parse(File.ReadAllText(projectLocation));
                    var version = projectXml.Descendants().FirstOrDefault(x => x.Name.Equals(XName.Get("PackageVersion")))?.Value;
                    projectVersions[project] = version??"unknown";
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }

            return projectVersions;
        }
    }
}