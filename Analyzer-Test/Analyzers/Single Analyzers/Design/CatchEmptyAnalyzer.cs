using Microsoft.CodeAnalysis;
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

        public static bool Analyze(SyntaxNode node)
        {
            var catchStatement = (CatchClauseSyntax)node;

            if (catchStatement == null || catchStatement.Declaration != null) return false;
            if (catchStatement.Block?.Statements.Count == 0) return true;
            return true;
        }

        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            throw new NotImplementedException();
        }
    }
}
