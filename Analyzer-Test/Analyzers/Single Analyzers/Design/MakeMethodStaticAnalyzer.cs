using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Analyzer_Test.Analyzers.Design
{
    class MakeMethodStaticAnalyzer : AbstractAnalyzer
    {

        public MakeMethodStaticAnalyzer()
        {
            Title = "Use static method";
            Description = "If the method is not referencing any instance variable and if you are " +
           "not creating a virtual, abstract, new or partial method, and if it is not a method override, " +
           "your instance method may be changed to a static method.";
        }

        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            var method = (MethodDeclarationSyntax)node;
            var syntaxList = new List<SyntaxKind> {SyntaxKind.StaticKeyword,
                SyntaxKind.PartialKeyword,
                SyntaxKind.VirtualKeyword,
                SyntaxKind.NewKeyword,
                SyntaxKind.AbstractKeyword,
                SyntaxKind.OverrideKeyword };
            if (method.Modifiers.Any(e => syntaxList.Contains(e.Kind())))
                return false;


            if (method.ExplicitInterfaceSpecifier != null)
                return false;
            var semanticModel = si.GetCurrentCompilation().GetSemanticModel(node.SyntaxTree);
            var methodSymbol = semanticModel.GetDeclaredSymbol(method);
            if (methodSymbol == null) return false;
            //if (methodSymbol.IsImplementingInterface()) return false;

            if (method.Body == null)
            {
                if (method.ExpressionBody?.Expression == null) return false;
                var dataFlowAnalysis = semanticModel.AnalyzeDataFlow(method.ExpressionBody.Expression);
                if (!dataFlowAnalysis.Succeeded) return false;
                if (dataFlowAnalysis.DataFlowsIn.Any(inSymbol => inSymbol.Name == "this")) return false;
            }
            else if (method.Body.Statements.Any())
            {
                var dataFlowAnalysis = semanticModel.AnalyzeDataFlow(method.Body);
                if (!dataFlowAnalysis.Succeeded) return false;
                if (dataFlowAnalysis.DataFlowsIn.Any(inSymbol => inSymbol.Name == "this")
                    || dataFlowAnalysis.WrittenInside.Any(inSymbol => inSymbol.Name == "this")) return false;
            }

            //if (IsTestMethod(method, methodSymbol)) return;
            //if (IsWebFormsMethod(methodSymbol)) return;
            //if (IsGetEnumerator(methodSymbol)) return;
            //if (HasRoutedEventArgs(methodSymbol)) return;

            return true;
        }

        public override bool CheckConditionals(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.MethodDeclaration))
                return true;
            return false;
        }
    }
}
