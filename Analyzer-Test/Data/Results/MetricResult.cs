using Microsoft.CodeAnalysis.CodeMetrics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Results
{
    public class MetricResult : Result
    {
        public ImmutableArray<(string, CodeAnalysisMetricData)> metric;
    }
}
