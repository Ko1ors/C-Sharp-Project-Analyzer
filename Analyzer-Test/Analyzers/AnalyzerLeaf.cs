using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public class AnalyzerLeaf : AnalyzerBase
    {
        public AnalyzerLeaf(AbstractAnalyzer analyzer) : base(analyzer)
        {
        }

    }
}
