using Analyzer_Test.Data;
using Analyzer_Test.Results;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    public abstract class ProjectHandler
    {
        /// <summary>
        /// Stores a reference to the next handler
        /// </summary>
        protected ProjectHandler handler;

        /// <summary>
        /// Set the next handler
        /// </summary>
        /// <param name="handler">Used to presents the next handler</param>
        public void SetHandler(ProjectHandler handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="si">Used to presents the information about solution</param>
        /// <returns>the handler result</returns>
        public abstract ProjectHandlerResult Handle(SolutionInfo si);


        public static ProjectHandler SetHandlers()
        {
            var handler = new WorkspaceHandler();
            var sh = new SolutionHandler();
            var ch = new CompilationHandler();
            var mh = new MetricHandler();
            handler.SetHandler(sh);
            sh.SetHandler(ch);
            ch.SetHandler(mh);
            return handler;
        }
    }
}
