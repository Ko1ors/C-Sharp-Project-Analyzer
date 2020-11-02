using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers.Single_Analyzers.Reliability
{
    public class UseConfigureAwaitFalseAnalyzer : AbstractAnalyzer
    {
        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            if (node.IsGenerated()) return false;
            var awaitExpression = (AwaitExpressionSyntax)node;
            var awaitedExpression = awaitExpression.Expression;
            if (!IsTask(awaitedExpression, node, si))
                return false;
            return true;
        }
        private static bool IsTask(ExpressionSyntax expression, SyntaxNode node, Data.SolutionInfo si)
        {
            var semanticModel = si.GetCurrentCompilation().GetSemanticModel(node.SyntaxTree);
            var type = semanticModel.GetTypeInfo(expression).Type as INamedTypeSymbol;
            if (type == null)
                return false;
            INamedTypeSymbol taskType;

            if (type.IsGenericType)
            {
                type = type.ConstructedFrom;
                taskType = semanticModel.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1");
            }
            else
            {
                taskType = semanticModel.Compilation.GetTypeByMetadataName("System.Threading.Tasks.Task");
            }
            return type.Equals(taskType);
        }

        public override bool CheckConditionals(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.AwaitExpression))
                return true;
            return false;
        }
    }
}
