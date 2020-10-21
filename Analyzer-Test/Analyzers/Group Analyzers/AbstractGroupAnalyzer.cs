using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers.Group_Analyzers
{
    public abstract class AbstractGroupAnalyzer : AbstractAnalyzer
    {
        public Dictionary<SyntaxNode, List<String>> report { get; protected set; } = new Dictionary<SyntaxNode, List<string>>();
        public List<AbstractAnalyzer> analyzers { get; protected set; }
    }
}
