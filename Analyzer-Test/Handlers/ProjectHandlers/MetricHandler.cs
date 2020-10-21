using Analyzer_Test.Data;
using Analyzer_Test.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class MetricHandler : ProjectHandler
    {
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            var m = ProjectCreator.TryComputeSolutionMetric(si?.sln);
            if(handler != null)
            {
                var phr = handler.Handle(si);
                if(phr.Metric == null && m != null)
                    phr.Metric = m;
                return phr;
            }
            return m == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "4", Message = "Metric wasn`t created" } : new ProjectHandlerResult() { SolutionInfo = si, Metric = m, Status = "5", Message = "Metric was created" };
        }
    }
}
