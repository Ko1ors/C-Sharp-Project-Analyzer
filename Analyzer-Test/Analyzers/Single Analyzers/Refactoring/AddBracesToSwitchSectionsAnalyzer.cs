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
    public class AddBracesToSwitchSectionsAnalyzer : AbstractAnalyzer
    {
        public override bool Analyze(SyntaxNode node, Data.SolutionInfo si)
        {
            if (node.IsGenerated()) return false;
            var @switch = (SwitchStatementSyntax)node;
            if (!@switch.Sections.All(HasBraces))
                return true;
            return false;
        }

        private static bool HasBraces(SwitchSectionSyntax section)
        {
            switch (section.Statements.Count)
            {
                case 1:
                    if (section.Statements.First() is BlockSyntax)
                        return true;
                    break;
                case 2:
                    if (section.Statements.First() is BlockSyntax && section.Statements.Last() is BreakStatementSyntax)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }

        public override bool CheckConditionals(SyntaxNode node)
        {
            if (node.IsKind(SyntaxKind.SwitchStatement))
                return true;
            return false;
        }
    }
}
