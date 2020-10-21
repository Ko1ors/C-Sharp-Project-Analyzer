﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public class AnalyzerBase
    {
        public AbstractAnalyzer analyzer;

        private Data.SolutionInfo solutionInfo;

        private SyntaxNode node;

        public AnalyzerBase(AbstractAnalyzer analyzer, SyntaxNode node, Data.SolutionInfo si)
        {
            this.analyzer = analyzer;
            this.node = node;
            solutionInfo = si;
        }

        public void SetNode(SyntaxNode node)
        {
            this.node = node;
        }

        public void SetSolutionInfo(Data.SolutionInfo si)
        {
            solutionInfo = si;
        }

        public bool Analyze()
        {
            return analyzer.Analyze(node,solutionInfo);
        }
    }
}
