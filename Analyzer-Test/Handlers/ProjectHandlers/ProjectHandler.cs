using Analyzer_Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    public abstract class ProjectHandler
    {
		protected ProjectHandler handler;

		public void SetHandler(ProjectHandler handler)
		{
			this.handler = handler;
		}

		public abstract void Handle(SolutionInfo si);
	}
}
