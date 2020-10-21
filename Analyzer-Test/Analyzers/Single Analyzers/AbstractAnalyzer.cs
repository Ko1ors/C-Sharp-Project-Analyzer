using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public abstract class AbstractAnalyzer
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }

        public abstract bool Analyze(SyntaxNode node, Data.SolutionInfo si);
    }
}
