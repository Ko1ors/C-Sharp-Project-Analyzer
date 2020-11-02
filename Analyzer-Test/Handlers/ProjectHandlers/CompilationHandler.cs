using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class CompilationHandler : ProjectHandler
    {
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            si?.SetCompilation(ProjectCreator.TryCompileSolution(si.sln));
            return si?.Compilation == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "4", Message = "Compilation wasn`t created" } : handler?.Handle(si) ?? new ProjectHandlerResult() { SolutionInfo = si, Status = "5", Message = "Compilation was created" };
        }
    }
}
