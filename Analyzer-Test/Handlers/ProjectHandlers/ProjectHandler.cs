using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    public abstract class ProjectHandler
    {
        protected ProjectHandler handler;

        public void SetHandler(ProjectHandler handler)
        {
            this.handler = handler;
        }

        public abstract ProjectHandlerResult Handle(SolutionInfo si);
    }
}
