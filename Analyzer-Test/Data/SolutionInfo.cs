using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Data
{
    public class SolutionInfo
    {
        public MSBuildWorkspace ws { get; private set; }
        public string solutionFilePath { get; set; }
        public Solution sln { get; private set; }
        public Compilation compilation { get; private set; }

        public void SetWorkspace(MSBuildWorkspace ws)
        {
            this.ws = ws;
        }

        public void SetSolution(Solution sln)
        {
            this.sln = sln;
        }

        public void SetCompilation(Compilation compilation)
        {
            this.compilation = compilation;
        }
    }
}
