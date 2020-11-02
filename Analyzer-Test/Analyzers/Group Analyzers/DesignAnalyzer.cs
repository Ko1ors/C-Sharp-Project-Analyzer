using Analyzer_Test.Analyzers.Group_Analyzers;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Analyzer_Test.Analyzers.Design
{
    public class DesignAnalyzers : AbstractGroupAnalyzer
    {

        public DesignAnalyzers()
        {
            analyzers = new List<AbstractAnalyzer>()
            {
            new CatchEmptyAnalyzer(),
            new MakeMethodStaticAnalyzer()
            };
        }

        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            foreach (AbstractAnalyzer analyzer in analyzers)
            {
                if (analyzer.CheckConditionals(node))
                    if (analyzer.Analyze(node, si))
                        ReportAdd(GetNodeClass(node), analyzer.GetResult());
            }
            if (report.Count > 0)
                return true;
            return false;
        }
    }
}
