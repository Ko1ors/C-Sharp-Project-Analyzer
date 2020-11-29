using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.Linq;

namespace Analyzer_Test.Data
{
    public class SolutionInfo : SolutionShortInfo
    {
        [JsonIgnoreAttribute]
        public MSBuildWorkspace ws { get; private set; }

        [JsonIgnoreAttribute]
        public Solution sln { get; private set; }

        [JsonIgnoreAttribute]
        public ImmutableArray<(string, Compilation)>? Compilation { get; private set; }

        [JsonIgnoreAttribute]
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
