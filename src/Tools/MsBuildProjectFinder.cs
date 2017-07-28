using System;
using System.IO;
using System.Linq;

namespace DotNetProver.Tools
{
    public class MsBuildProjectFinder
    {
        public static string FindMsBuildProject(string searchBase, string project)
        {
            var projectPath = project ?? searchBase;

            if (!Path.IsPathRooted(projectPath))
            {
                projectPath = Path.Combine(searchBase, projectPath);
            }

            if (Directory.Exists(projectPath))
            {
                var projects = Directory.EnumerateFileSystemEntries(projectPath, "*.*proj", SearchOption.TopDirectoryOnly)
                    .Where(f => !".xproj".Equals(Path.GetExtension(f), StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (projects.Length > 1)
                {
                    throw new FileNotFoundException();
                }

                if (projects.Length == 0)
                {
                    throw new FileNotFoundException();
                }

                return projects[0];
            }

            if (!File.Exists(projectPath))
            {
                throw new FileNotFoundException();
            }

            return projectPath;
        }
    }
}