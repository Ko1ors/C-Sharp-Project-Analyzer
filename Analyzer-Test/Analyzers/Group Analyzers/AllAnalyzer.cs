using Analyzer_Test.Analyzers.Group_Analyzers;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    class AllAnalyzers : AbstractGroupAnalyzer
    {
        public Compilation compilation;

        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            throw new NotImplementedException();
        }
    }
}
