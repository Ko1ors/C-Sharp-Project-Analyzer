using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class MetricHandler : ProjectHandler
    {
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            var m = ProjectExtension.TryComputeSolutionMetric(si?.Compilation);
            if (handler != null)
            {
                var phr = handler.Handle(si);
                if (phr.Metric == null && m != null)
                    phr.Metric = m;
                return phr;
            }
            return m == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "6", Message = "Metric wasn`t created" } : new ProjectHandlerResult() { SolutionInfo = si, Metric = m, Status = "7", Message = "Metric was created" };
        }
    }
}
