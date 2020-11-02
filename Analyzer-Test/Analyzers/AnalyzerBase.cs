using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Analyzer_Test.Analyzers
{
    public abstract class AnalyzerBase
    {
        public AbstractAnalyzer analyzer;


        public AnalyzerBase(AbstractAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        public bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            return analyzer.Analyze(node, si);
        }

        public virtual void Add(AnalyzerBase c) { }
        public virtual void Remove(AnalyzerBase c) { }

        public virtual List<AnalyzerBase> GetChildren()
        {
            return null;
        }

        public virtual string GetName()
        {
            return analyzer.ToString();
        }
    }
}
