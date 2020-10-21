using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public class AnalyzerLeaf : AnalyzerBase
    {
        public AnalyzerLeaf(AbstractAnalyzer analyzer, SyntaxNode node, Data.SolutionInfo si) : base(analyzer, node, si)
        {
        }

        public override void Add(AnalyzerBase c)
        {
            throw new Exception("Cannot add to a leaf");
        }

        public override void Remove(AnalyzerBase c)
        {
            throw new Exception("Cannot remove from a leaf");
        }
    }
}
