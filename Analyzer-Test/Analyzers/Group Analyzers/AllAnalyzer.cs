using Analyzer_Test.Analyzers.Group_Analyzers;
using Microsoft.CodeAnalysis;
using System;

namespace Analyzer_Test.Analyzers
{
    class AllAnalyzers : AbstractGroupAnalyzer
    {

        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            throw new NotImplementedException();
        }
    }
}
