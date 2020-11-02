using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers.Single_Analyzers.Usage
{
    public class AbstractClassShouldNotHavePublicCtorsAnalyzer : AbstractAnalyzer
    {
        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            if (node.IsGenerated()) 
                return false;
            var ctor = (ConstructorDeclarationSyntax)node;
            if (!ctor.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword))) 
                return false;
            var @class = ctor.Ancestors().FirstOrDefault() as ClassDeclarationSyntax;
            if (@class == null) 
                return false;
            if (!@class.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword))) 
                return false;
            return true;
        }

        public override bool CheckConditionals(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.ConstructorDeclaration))
                return true;
            return false;
        }
    }
}
