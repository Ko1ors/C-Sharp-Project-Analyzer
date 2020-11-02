using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers.Single_Analyzers.Perfomance
{
    class EmptyFinalizerAnalyzer : AbstractAnalyzer
    {
        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            if (node.IsGenerated()) return false;
            var finalizer = (DestructorDeclarationSyntax)node;
            var body = finalizer.Body;
            if (body == null) 
                return false;
            if (body.DescendantNodes().Any(n => !n.IsKind(SyntaxKind.SingleLineCommentTrivia | SyntaxKind.MultiLineCommentTrivia))) 
                return false;
            return true;
        }
    }
}
