using Analyzer_Test.Data.Results;
using Microsoft.CodeAnalysis;

namespace Analyzer_Test.Analyzers
{
    public abstract class AbstractAnalyzer
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }

        public abstract bool Analyze(SyntaxNode node, Data.SolutionInfo si);

        public AnalyzerResult GetResult()
        {
            return new AnalyzerResult() { AnalyzerType = this.GetType(), Status = "1", Title = Title, Message = Description };
        }

        public virtual bool CheckConditionals(SyntaxNode node) => true;
    }
}
