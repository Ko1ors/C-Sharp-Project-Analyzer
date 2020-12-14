using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class CompilationHandler : ProjectHandler
    {
        /// <summary>
        /// Process the request to compile a solution
        /// </summary>
        /// <param name="si">Used to presents the information about solution</param>
        /// <returns>the handler result</returns>
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            si?.SetCompilation(ProjectExtension.TryCompileSolution(si.sln));
            return si?.Compilation == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "4", Message = "Compilation wasn`t created" } : handler?.Handle(si) ?? new ProjectHandlerResult() { SolutionInfo = si, Status = "5", Message = "Compilation was created" };
        }
    }
}
