using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public class CatchEmptyAnalyzer : AbstractAnalyzer
    {
        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            var catchStatement = (CatchClauseSyntax)node;

            if (catchStatement == null || catchStatement.Declaration != null) return false;
            if (catchStatement.Block?.Statements.Count == 0) return false;
            return true;
        }

        public override bool CheckConditionals(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.CatchClause))
                return true;
            return false;
        }
    }
}
