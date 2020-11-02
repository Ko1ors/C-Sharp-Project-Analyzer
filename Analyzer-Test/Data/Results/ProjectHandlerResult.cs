using Analyzer_Test.Data;
using Microsoft.CodeAnalysis.CodeMetrics;
using System.Collections.Immutable;

namespace Analyzer_Test.Results
{
    public class ProjectHandlerResult : Result
    {
        public ImmutableArray<(string, CodeAnalysisMetricData)>? Metric;

        public SolutionInfo SolutionInfo;
    }
}
