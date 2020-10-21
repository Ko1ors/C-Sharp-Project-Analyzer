using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers.Design
{
    class DesignAnalyzers
    {
        private static Dictionary<SyntaxNode, List<String>> report = new Dictionary<SyntaxNode, List<string>>();
        public static void Analyze(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.CatchClause))
            {
                if (CatchEmptyAnalyzer.Analyze(node))
                {
                    ReportAdd(GetNodeClass(node), "Contains empty catch.");
                }
            }

            if (node.IsKind(SyntaxKind.MethodDeclaration))
            {
                if (MakeMethodStaticAnalyzer.Analyze(node))
                    ReportAdd(GetNodeClass(node), MakeMethodStaticAnalyzer.Title);
            }
        }

        private static void ReportAdd(SyntaxNode node, String result)
        {
            if (report.TryGetValue(node, out var results))
                results.Add(result);
            else
                report.Add(node,new List<string> { result });
            Console.WriteLine((node as TypeDeclarationSyntax).Identifier.ValueText);
        }

        public static SyntaxNode GetNodeClass(SyntaxNode node)
        {
            var tree = node.SyntaxTree;
            if (tree is null)
                throw new Exception();
            return tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Last();
        }

        public static Dictionary<SyntaxNode, List<String>> GetReport()
        {
            return report;
        }
    }
}
