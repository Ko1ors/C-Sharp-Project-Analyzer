﻿using Analyzer_Test.Data;
using Analyzer_Test.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer_Test.Handlers.ProjectHandlers
{
    class SolutionHandler : ProjectHandler
    {
        public override ProjectHandlerResult Handle(SolutionInfo si)
        {
            si?.SetSolution(ProjectCreator.TryOpenSolution(si.ws,si.solutionFilePath));
            return si?.sln == null ? new ProjectHandlerResult() { SolutionInfo = si, Status = "2", Message = "Solution wasn`t created" } : handler?.Handle(si) ?? new ProjectHandlerResult() { SolutionInfo = si, Status = "3", Message = "Solution was created" };
        }
    }
}
