using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Immutable;
using System.Linq;

namespace Analyzer_Test.Data
{
    public class SolutionInfo
    {
        public MSBuildWorkspace ws { get; private set; }
        public string solutionFilePath { get; set; }
        public Solution sln { get; private set; }
        public ImmutableArray<(string, Compilation)>? Compilation { get; private set; }

        public string currentProject;

        public void SetWorkspace(MSBuildWorkspace ws)
        {
            this.ws = ws;
        }

        public Compilation GetCurrentCompilation()
        {
            return Compilation?.First(e => e.Item1.Equals(currentProject)).Item2;
        }

        public void SetSolution(Solution sln)
        {
            this.sln = sln;
        }

        public void SetCompilation(ImmutableArray<(string, Compilation)>? compilation)
        {
            this.Compilation = compilation;
        }
    }
}
