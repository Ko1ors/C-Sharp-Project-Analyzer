using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class SolutionHandler : ProjectHandler
    {
        /// <summary>
        /// Process the request to open a solution
        /// </summary>
        /// <param name="si">Used to presents the information about solution</param>
        /// <returns>the handler result</returns>
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            si?.SetSolution(ProjectExtension.TryOpenSolution(si.ws, si.solutionFilePath));
            return si?.sln == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "2", Message = "Solution wasn`t created" } : handler?.Handle(si) ?? new ProjectHandlerResult() { SolutionInfo = si, Status = "3", Message = "Solution was created" };
        }
    }
}
