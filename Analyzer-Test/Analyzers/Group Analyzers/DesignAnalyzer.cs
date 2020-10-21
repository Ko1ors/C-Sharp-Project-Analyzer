using Analyzer_Test.Analyzers.Group_Analyzers;
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
    public class DesignAnalyzers : AbstractGroupAnalyzer
    {

        public DesignAnalyzers()
        {
            analyzers = new List<AbstractAnalyzer>();
        }
       
        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
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
                var mmsa = new MakeMethodStaticAnalyzer();
                if (mmsa.Analyze(node,si))
                    ReportAdd(GetNodeClass(node), mmsa.Title);
            }
            return true;
        }

        private void ReportAdd(SyntaxNode node, String result)
        {
            if (report.TryGetValue(node, out var results))
                results.Add(result);
            else
                report.Add(node,new List<string> { result });
            Console.WriteLine((node as TypeDeclarationSyntax).Identifier.ValueText);
        }

        public  SyntaxNode GetNodeClass(SyntaxNode node)
        {
            var tree = node.SyntaxTree;
            if (tree is null)
                throw new Exception();
            return tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Last();
        }

        public Dictionary<SyntaxNode, List<String>> GetReport()
        {
            return report;
        }

    }
}
