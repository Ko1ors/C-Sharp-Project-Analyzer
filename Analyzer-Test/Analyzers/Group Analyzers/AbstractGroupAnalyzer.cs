using Analyzer_Test.Data.Results;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers.Group_Analyzers
{
    public abstract class AbstractGroupAnalyzer : AbstractAnalyzer
    {
        public Dictionary<SyntaxNode, List<AnalyzerResult>> report { get; protected set; } = new Dictionary<SyntaxNode, List<AnalyzerResult>>();
        public List<AbstractAnalyzer> analyzers { get; protected set; }

        protected void ReportAdd(SyntaxNode node, AnalyzerResult result)
        {
            if (report.TryGetValue(node, out var results))
                results.Add(result);
            else
                report.Add(node, new List<AnalyzerResult> { result });
            Console.WriteLine((node as TypeDeclarationSyntax).Identifier.ValueText);
        }

        protected SyntaxNode GetNodeClass(SyntaxNode node)
        {
            var tree = node.SyntaxTree;
            if (tree is null)
                throw new Exception();
            return tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Last();
        }

        public Dictionary<SyntaxNode, List<AnalyzerResult>> GetReport()
        {
            return report;
        }
    }
}
