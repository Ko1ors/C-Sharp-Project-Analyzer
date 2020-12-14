using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class WorkspaceHandler : ProjectHandler
    {
        /// <summary>
        /// Process the request to create a workspace
        /// </summary>
        /// <param name="si">Used to presents the information about solution</param>
        /// <returns>the handler result</returns>
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            si?.SetWorkspace(ProjectExtension.CreateWorkspace());
            return si?.ws == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "0", Message = "Workspace wasn`t created" } : handler?.Handle(si) ?? new ProjectHandlerResult() { SolutionInfo = si, Status = "1", Message = "Workspace was created" };
        }
    }
}
