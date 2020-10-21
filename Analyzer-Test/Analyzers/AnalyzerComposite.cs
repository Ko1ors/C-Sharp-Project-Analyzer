using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public class AnalyzerComposite : AnalyzerBase
    {

        public AnalyzerComposite(AbstractAnalyzer analyzer, SyntaxNode node, Data.SolutionInfo si) : base(analyzer,node,si)
        {
        }

        public override void Add(AnalyzerBase c)
        {
            throw new NotImplementedException();
        }

        public override void Remove(AnalyzerBase c)
        {
            throw new NotImplementedException();
        }
    }
}
