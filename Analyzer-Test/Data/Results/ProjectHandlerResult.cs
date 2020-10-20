using Analyzer_Test.Data;
using Microsoft.CodeAnalysis.CodeMetrics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Results
{
    public class ProjectHandlerResult : Result
    {
        public ImmutableArray<(string, CodeAnalysisMetricData)> Metric;

        public SolutionInfo SolutionInfo;
    }
}
