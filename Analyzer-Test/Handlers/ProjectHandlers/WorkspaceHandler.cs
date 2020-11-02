using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class WorkspaceHandler : ProjectHandler
    {
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            si?.SetWorkspace(ProjectCreator.CreateWorkspace());
            return si?.ws == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "0", Message = "Workspace wasn`t created" } : handler?.Handle(si) ?? new ProjectHandlerResult() { SolutionInfo = si, Status = "1", Message = "Workspace was created" };
        }
    }
}
