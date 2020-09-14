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
        public static void Analyze(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.CatchClause))
            {
                if (CatchEmptyAnalyzer.Analyze(node))
                    Console.WriteLine(node.Parent.Parent);
            }
        }

    }
}
