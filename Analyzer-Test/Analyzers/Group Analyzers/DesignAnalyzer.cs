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

        

    }
}
