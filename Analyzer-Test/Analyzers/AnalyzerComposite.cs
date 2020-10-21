using Analyzer_Test.Analyzers.Group_Analyzers;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Analyzers
{
    public class AnalyzerComposite : AnalyzerBase
    {
        private List<AnalyzerBase> children = new List<AnalyzerBase>();
        public AnalyzerComposite(AbstractGroupAnalyzer analyzer) : base(analyzer)
        {
        }

        public override void Add(AnalyzerBase c)
        {
            children.Add(c);
        }

        public override void Remove(AnalyzerBase c)
        {
            children.Remove(c);
        }

        public override List<AnalyzerBase> GetChildren()
        {
            return children;
        }
    }
}
